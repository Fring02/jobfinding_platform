import React, { useRef, useReducer, useContext, useState } from 'react';
import '../AuthForm.css';
import {loginReducer} from '../../../validation/loginFormReducer';
import AuthContext from '../../../store/auth_context';
const SigninForm = () => {
  const ctx = useContext(AuthContext);

  const emailRef = useRef();
  const passwordRef = useRef();
  const [role, setRole] = useState('');
  const [data, dispatch] = useReducer(loginReducer, {});
  const onEmailChange = () => dispatch({type: 'EMAIL', payload: emailRef.current.value});
  const onPasswordChange = () => dispatch({type: 'PASSWORD', payload: passwordRef.current.value});
  const onLogin = (e) => {
    if(role === '') ctx.setAuthError("Choose, who are you");
    else ctx.onLogin(e, {email: emailRef.current.value, password: passwordRef.current.value}, role);
  }

    return <section className="vh-100">
    <div className="container h-100">
      <div className="row d-flex justify-content-center align-items-center h-100">
        <div className="col-lg-12 col-xl-11">
          <div className="card text-black signupForm">
            <div className="card-body p-md-5">
              <div className="row justify-content-center">
                <div className="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">
  
                  <p className="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Sign in</p>
                  {data.error !== '' && <div className='alert alert-danger'>{data.error}</div>}
                  {ctx.authError !== '' && <div className='alert alert-danger'>{ctx.authError}</div>}
                  <form className="mx-1 mx-md-4" onSubmit={onLogin}>

                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-envelope fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example3c">Email</label>
                        <input onInput={onEmailChange} ref={emailRef} type="email" id="form3Example3c" className="form-control" />
                      </div>
                    </div>
  
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-lock fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example4c">Password</label>
                        <input onInput={onPasswordChange} ref={passwordRef} type="password" id="form3Example4c" className="form-control" />
                      </div>
                    </div>

                    <div className='row text-center'>
                      <div className="card col-lg-6 student">
                        <img src="https://www.pngkey.com/png/full/204-2043794_male-icon-png-student-icon.png"
                        className="card-img-top roleImage" alt="" />
                        <div className="card-body">
                          <h5 className="card-title">Student</h5>
                          <div className="form-check text-center d-flex justify-content-center">
                            <input className="form-check-input" type="radio" name="role" value="student"
                             onChange={() => setRole('student')} />
                          </div>
                        </div>
                      </div>
                      <div className="card col-lg-6 employer">
                        <img src="https://cdn-icons-png.flaticon.com/512/5956/5956378.png" className="card-img-top roleImage" alt="..." />
                        <div className="card-body">
                          <h5 className="card-title mb-4">Employer</h5>
                          <div className="form-check text-center d-flex justify-content-center mt-2">
                            <input className="form-check-input" type="radio" name="role" value="employer"
                             onChange={() => setRole('employer')} />
                          </div>
                          </div>
                        </div>
                      </div>

                    <div className="d-flex justify-content-center mx-4 mb-3 mb-lg-4">
                        <button type="submit" className="btn btn-info btn-lg">
                            Sign in
                          </button>
                    </div>
  
                  </form>
  
                </div>
                <div className="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">
  
                  <img src="https://upload.wikimedia.org/wikipedia/commons/d/db/Zeronet_logo.png"
                    className="img-fluid" alt="" />
  
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>;
}

export default SigninForm;