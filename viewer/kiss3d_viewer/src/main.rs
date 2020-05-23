/* On basis of example/persistent_point_cloud.rs */

extern crate kiss3d;
extern crate nalgebra as na;

use na::{Vector3, Matrix4, Point2, Point3, UnitQuaternion};

use kiss3d::camera::Camera;
use kiss3d::context::Context;
use kiss3d::planar_camera::PlanarCamera;
use kiss3d::post_processing::PostProcessingEffect;
use kiss3d::renderer::Renderer;
use kiss3d::text::Font;
use kiss3d::window::{State, Window};
use kiss3d::light::Light;
use kiss3d::resource::{
    AllocationType, BufferType, Effect, GPUVec, ShaderAttribute, ShaderUniform,
};

const VERTEX_SHADER_SRC: &'static str = "#version 100
    attribute vec3 position;
    attribute vec3 color;
    varying   vec3 Color;
    uniform   mat4 projection;
    uniform   mat4 view;
    void main() {
        gl_Position = projection * view * vec4(position, 1.0);
        Color = color;
    }";

const FRAGMENT_SHADER_SRC: &'static str = "#version 100
#ifdef GL_FRAGMENT_PRECISION_HIGH
   precision highp float;
#else
   precision mediump float;
#endif

    varying vec3 Color;
    void main() {
        gl_FragColor = vec4(Color, 1.0);
    }";

// Custom renderers are used to allow rendering objects that are not necessarily
// represented as meshes. In this example, we will render a large, growing, point cloud
// with a color associated to each point.

// Writing a custom renderer requires the main loop to be
// handled by the `State` trait instead of a `while window.render()`
// like other examples.

// Seems compile to wasm and native platform also require a custom renderer.

// Structure which manages the display of long-living points
// All essential components of a renderer.
struct PointCloudRenderer {
    shader:          Effect,                                // vs and fs
    pos:             ShaderAttribute<Point3<f32>>,
    color:           ShaderAttribute<Point3<f32>>,
    projection:      ShaderUniform<Matrix4<f32>>,
    view:            ShaderUniform<Matrix4<f32>>,
    color3d_points:  GPUVec<Point3<f32>>,                   // Seems it is a vector which contains all points and their color.
    // After every point following its color.
    point_size:      f32,                                   // Rasterized diameter of point. `glPointSize(GLfloat size)`.
    // https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/glPointSize.xhtml
}

impl PointCloudRenderer {
    fn new(point_size: f32) -> PointCloudRenderer {
        let mut shader = Effect::new_from_str(VERTEX_SHADER_SRC, FRAGMENT_SHADER_SRC);

        shader.use_program();

        PointCloudRenderer {
            pos:            shader.get_attrib::<Point3<f32>>("position").unwrap(),
            color:          shader.get_attrib::<Point3<f32>>("color").unwrap(),
            projection:     shader.get_uniform::<Matrix4<f32>>("projection").unwrap(),
            view:           shader.get_uniform::<Matrix4<f32>>("view").unwrap(),
            color3d_points: GPUVec::new(Vec::new(), BufferType::Array, AllocationType::StreamDraw),
            shader,
            point_size,

        }
    }

    fn push_point(&mut self, point: Point3<f32>, color: Point3<f32>) {
        // Rust study: Where color3d_points from? What does `if let Some(x) = y` do??
        if let Some(color3d_points) = self.color3d_points.data_mut() {
            color3d_points.push(point);
            color3d_points.push(color);
        }
    }

    fn points_count(&self) -> usize { self.color3d_points.len() / 2 }
}

// Rust study: Implement Renderer trait to PointCloudRenderer.
impl Renderer for PointCloudRenderer {
    // Draw
    // What is `dyn`??
    // Is it collection of all objects need to be rendered??
    fn render(&mut self, pass: usize, camera: &mut dyn Camera) {
        if self.color3d_points.len() == 0 {
            return;
        }

        // Activate shader program.
        self.shader.use_program();
        // Enable attributes
        self.pos.enable();
        self.color.enable();

        // Is it also update reference of `pass` to GPU??
        camera.upload(pass, &mut self.projection, &mut self.view);

        // Bind data to GPU buffer.
        // Is it sub buffer binding?? Where does it allocate total buffer size?? Need to inspect process inside to understand.
        self.pos.bind_sub_buffer(&mut self.color3d_points, 1, 0);
        self.color.bind_sub_buffer(&mut self.color3d_points, 1, 1);

        // Get context
        let ctxt = Context::get();
        ctxt.point_size(self.point_size);
        ctxt.draw_arrays(Context::POINTS, 0, (self.color3d_points.len() / 2) as i32);

        self.pos.disable();
        self.color.disable();
    }
}

// State
struct AppState {
    point_cloud_renderer: PointCloudRenderer,
}

impl State for AppState {
    // Return the custom renderer that will be called at each render loop.
    fn cameras_and_effect_and_renderer(
        &mut self,
    ) -> (
        Option<&mut dyn Camera>,
        Option<&mut dyn PlanarCamera>,
        Option<&mut dyn Renderer>,
        Option<&mut dyn PostProcessingEffect>,
    ) { (None, None, Some(&mut self.point_cloud_renderer), None) }

    fn step(&mut self, window: &mut Window) {
        if self.point_cloud_renderer.points_count() < 1_000_000 {
            // Add some random points to the point cloud
            for _ in 0..1_000 {
                let random: Point3<f32> = rand::random();
                self.point_cloud_renderer.push_point((random - Vector3::repeat(0.5)) * 0.5, rand::random());
            }
        }

        let num_points_text = format!(
            "Number of points: {}",
            self.point_cloud_renderer.points_count()
        );

        window.draw_text(
            &num_points_text,
            &Point2::new(0.0, 20.0),
            60.0,
            &Font::default(),
            &Point3::new(1.0, 1.0, 1.0),
        )
    }
}

fn main() {
    let mut window = Window::new("Points cloud: Kiss3d");
    let app = AppState {
        point_cloud_renderer: PointCloudRenderer::new(4.0),
    };

    window.render_loop(app)
}
