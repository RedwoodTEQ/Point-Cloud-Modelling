<template><v-layout row wrap class="pt-4">

  <!-- login -->

  <v-flex md6 v-if="!confirm" class="pa-3">
    <section title="Login to My Website">
      <v-form @submit.prevent.stop="userLogin()" ref="form-login" autocomplete="off">
        <v-text-field v-model="loginEmail" label="Email" required autocomplete="off" :rules="emailRules"></v-text-field>
        <v-text-field v-model="loginPassword" type="password" label="Password" required autocomplete="off" :rules="passwordRules"></v-text-field>
        <v-alert :value="$store.state.account.loginError" color="error" icon="far fa-exclamation-triangle" outline>{{ $store.state.account.loginError }}</v-alert>
        <v-btn color="primary" type="submit">Login</v-btn>
        <br><br>
        <span @click="confirm = true">Need to <span style="text-decoration: underline; color: #2196F3; cursor: pointer;">confirm</span> your email address?</span>
      </v-form>
    </section>
  </v-flex>

  <!-- signup -->

  <v-flex md6 v-if="!confirm" class="pa-3">
    <section title="Signup to My Website">
      <v-form @submit.prevent.stop="userSignup()" ref="form-signup" autocomplete="off">
        <v-text-field v-model="signupEmail" label="Email address" required hint="We'll never share your email with anyone else." persistent-hint :rules="emailRules"></v-text-field>
        <v-text-field v-model="signupPassword" type="password" label="Password" required browser-autocomplete="new-password" :rules="passwordRules"></v-text-field>
        <v-text-field v-model="signupPasswordConfirm" type="password" label="Confirm Password" required browser-autocomplete="new-password" :rules="passwordRules"></v-text-field>
        <v-alert :value="signupError || $store.state.account.signupError" color="error" icon="far fa-exclamation-triangle" outline>{{ signupError ? signupError : $store.state.account.signupError }}</v-alert>
        <v-btn color="primary" type="submit">Signup</v-btn>
        <br><br>
        <v-alert :value="true" type="info" outline>You will be required to confirm your email by entering the code that has been emailed to you.</v-alert>
      </v-form>
    </section>
  </v-flex>

  <!-- confirm -->

  <v-flex md2 style="padding: 5%;" v-if="confirm"/>
  <v-flex md8 class="pa-3" v-if="confirm">
    <section title="Confirm Your Email">
      <v-form @submit.prevent.stop="userConfirm()" ref="form-confirm" autocomplete="off">
        <v-text-field v-model="confirmEmail" label="Email address" required browser-autocomplete="off" :rules="emailRules"></v-text-field>
        <v-text-field v-model="confirmConfirmation" type="text" browser-autocomplete="new-password" label="Confirmation Number" required hint="Check your email (and spam folder) for a confirmation Number." persistent-hint :rules="confirmationRules"></v-text-field>
        <v-alert v-if="$store.state.account.confirmError" color="error" icon="far fa-exclamation-triangle" outline>{{ $store.state.account.confirmError }}</v-alert>
        <v-btn color="primary" type="submit">Confirm</v-btn>
        <br><br>
        <v-alert :value="true" type="info" outline>You will need to login after confirming your email.</v-alert>
        <br>
        <span @click.stop="confirm = false" style="cursor: pointer;">Need to <span style="text-decoration: underline; color: #2196F3; cursor: pointer;">login or signup</span>?</span>
      </v-form>
    </section>
  </v-flex>
  <v-flex md2 style="padding: 5%;" v-if="confirm"/>

  <v-progress-linear :indeterminate="true" color="secondary" v-if="loading"></v-progress-linear>

</v-layout></template>

<script>
// Dependencies ===============
import section from '@/components/section'
// Core =======================
let component = {
  data: () => ({
    loginEmail: '',
    loginPassword: '',
    confirmEmail: '',
    confirmConfirmation: '',
    signupEmail: '',
    signupPassword: '',
    signupPasswordConfirm: '',
    signupError: '',
    loading: false,
    passwordRules: [
      v => !!v || 'Password is required',
      v => (v && v.length >= 8) || 'Password must be at least 8 characters',
    ],
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+/.test(v) || 'E-mail must be valid',
    ],
    confirmationRules: [
      v => (v && v.length >= 5) || 'Verification code must be at least 5 numbers',
    ],
  }),
  computed: {
    confirm: {
      get(){
        return this.$store.state.account.confirm
      },
      set(value){
        this.$store.commit('account/confirm', value)
      },
    },
  },
  methods: {
    async userLogin(){
      if(!this.$refs['form-login'].validate())
        return
      this.loading = true
      await this.$store.dispatch('account/login', {email: this.loginEmail, password: this.loginPassword})
      this.loading = false
      return false
    },
    async userSignup(){
      if(!this.$refs['form-signup'].validate())
        return
      if(this.signupPassword !== this.signupPasswordConfirm){
        this.signupError = 'Confirmation password must match'
        return
      }
      this.signupError = ''
      this.loading = true
      this.confirmEmail = this.signupEmail
      await this.$store.dispatch('account/signup', {email: this.signupEmail, password: this.signupPassword})
      this.loading = false
      return false
    },
    async userConfirm(){
      if(!this.$refs['form-confirm'].validate())
        return
      this.loading = true
      await this.$store.dispatch('account/confirm', {email: this.confirmEmail, code: this.confirmConfirmation})
      this.loading = false
      return false
    },
    noAutoComplete(){
      this.$el.querySelectorAll('input[type="text"][autocomplete="off"').forEach(it => {
        it.setAttribute('autocomplete', 'new-password')
      })
    },
  },
  components: {
    section,
  },
  async created(){
    this.confirm = this.$store.state.account.user && this.$store.state.account.user.attributes && !this.$store.state.account.user.attributes.email_verified
  },
  mounted(){
    this.noAutoComplete()
  },
}
// Export =====================
export default component
</script>

<style scoped>
button{
  background-color: #14A3F2;
  font-weight: 500;
  letter-spacing: -.5px;
}
div.alert{
  color: #990000;
}
</style>
