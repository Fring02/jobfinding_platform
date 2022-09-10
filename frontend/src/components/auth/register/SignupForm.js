import React, { Fragment, useContext, useEffect, useReducer, useRef, useState } from 'react';
import '../AuthForm.css';
import { registrationReducer } from '../../../validation/registerFormReducer';
import { register } from '../../../api/users_api';
import { getCompanies } from '../../../api/companies_api';
import Cookies from 'js-cookie';
import AuthContext from '../../../store/auth_context';
const SignupForm = () => {
    const ctx = useContext(AuthContext);
    const [companies, setCompanies] = useState([]);
    const [role, setRole] = useState('');
    useEffect(() => {
      getCompanies().then(response => {
        setCompanies(response.data);
      }).catch(err => ctx.setAuthError(err.message));
    }, [ctx]);

    const firstnameRef = useRef();
    const lastnameRef = useRef();
    const patronymicRef = useRef();
    const emailRef = useRef();
    const passwordRef = useRef();
    const repeatPasswordRef = useRef();
    const companyRef = useRef();
    const phoneRef = useRef();
    const universityRef = useRef();
    const facultyRef = useRef();
    const courseRef = useRef();

    const [data, dispatch] = useReducer(registrationReducer, {});


    const onFirstNameChange = () => dispatch({type: 'FIRSTNAME', payload: firstnameRef.current.value});
    const onLastNameChange = () => dispatch({type: 'LASTNAME', payload: lastnameRef.current.value});
    const onPatronymicChange = () => dispatch({type: 'PATRONYMIC', payload: patronymicRef.current.value});
    const onEmailChange = () => dispatch({type: 'EMAIL', payload: emailRef.current.value});
    const onPasswordChange = () => dispatch({type: 'PASSWORD', payload: passwordRef.current.value});
    const onRepeatPassword = () => dispatch({type: 'REPEAT_PASSWORD', payload: repeatPasswordRef.current.value});
    const onCompanyChange = () => dispatch({type: 'COMPANY', payload: companyRef.current.value});
    const onPhoneChange = () => dispatch({type: 'PHONE', payload: phoneRef.current.value});
    const onUniversityChange = () => dispatch({type: 'UNIVERSITY', payload: universityRef.current.value});
    const onFacultyChange = () => dispatch({type: 'FACULTY', payload: facultyRef.current.value});
    const onCourseChange = () => dispatch({type: 'COURSE', payload: courseRef.current.value});

    const onSignUp = (e) => {
      let user = data;
      e.preventDefault();
      for (const key in user)
        if(user[key] === '' && key !== 'error') {
          if(key === 'companyId'){
            ctx.setAuthError('Specify your company');
            return;
          }
          ctx.setAuthError('Fill all fields');
          return;
        }
      delete user.error;
      register(user, role).then(response => {
        window.location.href = '/';
      }).catch(err => ctx.setAuthError(err.message));
    }

    return <Fragment>
    <section className="vh-100">
    <div className="container h-100">
      <div className="row d-flex justify-content-center align-items-center h-100">
        <div className="col-lg-12 col-xl-11">
          <div className="card text-black signupForm">
            <div className="card-body p-md-5">
              <div className="row justify-content-center">
                <div className="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">
  
                  <p className="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Sign up</p>
                  {data.error !== '' && <div className='alert alert-danger'>{data.error}</div>}
                  {ctx.authError !== '' && <div className='alert alert-danger'>{ctx.authError}</div>}
                  <form className="mx-1 mx-md-4" onSubmit={onSignUp}>
  
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-user fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example1c">First name:</label>
                        <input onInput={onFirstNameChange} type="text" id="form3Example1c" className="form-control" ref={firstnameRef} />
                      </div>
                    </div>
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-user fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example1c">Last name:</label>
                        <input onInput={onLastNameChange} type="text" id="form3Example1c" className="form-control" ref={lastnameRef} />
                      </div>
                    </div>
                    
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-user fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example1c">Patronymic:</label>
                        <input onInput={onPatronymicChange} type="text" id="form3Example1c" className="form-control" ref={patronymicRef} />
                      </div>
                    </div>
  
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-envelope fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example3c">Your Email</label>
                        <input onInput={onEmailChange} type="email" id="form3Example3c" className="form-control" ref={emailRef} />
                      </div>
                    </div>

                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-envelope fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example3c">Your phone number</label>
                        <input onInput={onPhoneChange} type="text" id="form3Example3c" className="form-control" ref={phoneRef} />
                      </div>
                    </div>
  
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-lock fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example4c">Password</label>
                        <input onInput={onPasswordChange} type="password" id="form3Example4c" className="form-control" ref={passwordRef} />
                      </div>
                    </div>
  
                    <div className="d-flex flex-row align-items-center mb-4">
                      <i className="fas fa-key fa-lg me-3 fa-fw"></i>
                      <div className="form-outline flex-fill mb-0">
                        <label className="form-label" htmlFor="form3Example4cd">Repeat your password</label>
                        <input onInput={onRepeatPassword} type="password" id="form3Example4cd" className="form-control" ref={repeatPasswordRef} />
                      </div>
                    </div>

                    <div className='row text-center mt-5'>
                      <div className="card col-lg-6 student">
                        <img src="https://www.pngkey.com/png/full/204-2043794_male-icon-png-student-icon.png"
                        className="card-img-top roleImage" alt="" />
                        <div className="card-body">
                          <h5 className="card-title">Student</h5>
                          
                          <a className="btn btn-primary btn-sm" data-bs-toggle="collapse" href="#studentInfoCollapse" 
                          role="button" aria-expanded="false" aria-controls="studentInfoCollapse">Write some information about yourself:</a>
                          <div className="collapse mt-1" id="studentInfoCollapse">
                            <div className="card card-body" id='studentInfo'>
                              <div className='mb-3'>
                                <label for="university">University</label>
                                <input onInput={onUniversityChange} ref={universityRef} type='text' className='form-control' id='university' />
                              </div>
                              <div className='mb-3'>
                                <label for="faculty">Faculty</label>
                                <input onInput={onFacultyChange} ref={facultyRef} type='text' className='form-control' id='faculty' />
                              </div>
                              <div className='mb-3'>
                                <label for="course">Course</label>
                                <input onInput={onCourseChange} ref={courseRef} type='number' min={1} max={5} className='form-control' id='course' />
                              </div>
                          <div className="form-check text-center d-flex justify-content-center">
                            <input defaultChecked className="form-check-input" type="radio" name="role" onChange={() => setRole('student')}/>
                          </div>
                            </div>
                        </div>

                        </div>
                      </div>
                      <div className="card col-lg-6 employer">
                        <img src="https://cdn-icons-png.flaticon.com/512/5956/5956378.png" className="card-img-top roleImage" alt="..." />
                        <div className="card-body">
                          <h5 className="card-title mb-4">Employer</h5>
                          <a className="btn btn-primary btn-sm" data-bs-toggle="collapse" href="#companiesCollapse" 
                          role="button" aria-expanded="false" aria-controls="companiesCollapse">Choose company</a>

                          <div className="collapse mt-1" id="companiesCollapse">
                            <div className="card card-body" id='companySignUp'>
                              <select onChange={onCompanyChange} className="form-select" ref={companyRef}>
                                <option value="" selected>Choose...</option>
                                {companies.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                              </select>
                            </div>
                            
                          <div className="form-check text-center d-flex justify-content-center mt-2">
                            <input className="form-check-input" type="radio" name="role" 
                             onChange={() => setRole('employer')} />
                          </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="d-flex justify-content-center mt-4">
                        <button type="submit" className="btn btn-primary"
                        data-bs-toggle="collapse" data-bs-target="#roleCollapse" 
                        aria-expanded="false" aria-controls="roleCollapse">Sign up</button>
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
  </section>
  </Fragment>;
}

export default SignupForm;