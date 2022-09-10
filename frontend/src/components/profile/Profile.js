import jwtDecode from "jwt-decode";
import React, { useContext, useEffect, useRef, useState } from "react";
import { NavLink } from "react-router-dom";
import { getUser, sendEmailUpdateMessage, updateUser } from "../../api/users_api";
import AuthContext from "../../store/auth_context";
import {deleteAdvertisement} from '../../api/advertisements_api';
import { uploadResume, downloadResume, deleteResume } from "../../api/resumes_api";
import { deleteFromFeatured } from '../../api/featured_api';
import { getResponsesByCompany, deleteResponse } from '../../api/responses_api';
import AdvertisementUpdateForm from "./AdvertisementUpdateForm";
let download = require('downloadjs');
const Profile = () => {
  const ctx = useContext(AuthContext);
  const [user, setUser] = useState(null);
  const [responses, setResponses] = useState([]);
  const resumeRef = useRef();
  const phoneRef = useRef();
  const [error, setError] = useState("");
  const [isEmailUpdated, setIsEmailUpdated] = useState(false);
  const userId = jwtDecode(ctx.accessToken)[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    ];
    
  useEffect(() => {
    getUser(userId, ctx.role, ctx.accessToken).then((user) => {
      setUser(user);
      if(ctx !== undefined && ctx.role === 'employer'){
        getResponsesByCompany(user.company.id, ctx.accessToken).then(responses => setResponses(responses)).catch(e => setError(e.message));
      }
    }).catch((e) => setError(e.message));
    
  }, []);
  const onDeleteAdvertisement = (id) => {
  deleteAdvertisement(id, ctx.accessToken).then(ok => window.location.reload()).catch(e => setError(e.message));
  }

  const onResumeUpload = (e) => {
    e.preventDefault();
    const resume = resumeRef.current.files[0];
    if(resume){
      uploadResume(userId, resume).then(ok => window.location.reload()).catch(e => setError(e.message));
    } else setError('Please, upload resume');
  }

  const onResumeDownload = (e, id, resumeName = user.resume.name) => {
    e.preventDefault();
    downloadResume(id).then(r => {download(r, resumeName, 'application/pdf');}).catch(e => setError(e.message));
  }
  const onResumeDelete = (e) => {
    e.preventDefault();
    deleteResume(userId).then(ok => window.location.reload()).catch(e => setError(e.message));
  }

  const onUpdateEmail = (e) => {
    e.preventDefault();
    sendEmailUpdateMessage(userId, ctx.role, ctx.accessToken).then(ok => setIsEmailUpdated(true)).catch(e => setError(e.message));
  }

  const onUpdatePhone = (e) => {
    e.preventDefault();
    let phone = String(phoneRef.current.value);
    if(!phone.match(/^(\+7|7|8)?[\s\-]?\(?[0-9][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$/))
      setError('Phone number is in wrong format');
     else 
      updateUser(userId, ctx.role, {phone: phone}, ctx.accessToken).then(ok => {window.location.href = '/'})
      .catch(e => setError(e.message));
  }

  const onDeleteFromFeatured = (e, advertisementId) => {
    e.preventDefault();
    deleteFromFeatured(advertisementId, userId, ctx.accessToken).then(ok => {window.location.href = '/'}).catch(e => setError(e.message));
  }

  const onDeleteFromResponses = (e, advertisementId, studentId) => {
    e.preventDefault();
    deleteResponse(advertisementId, studentId, ctx.accessToken).then(ok => {window.location.href = '/'}).catch(e => setError(e.message));
  }

  return (
    <div className="container">
      {user !== null && (
        <div className="main-body">
          <div className="row gutters-sm">
            <div className="col-md-4 mb-3">
              <div className="card">
                <div className="card-body">
                  <div className="d-flex flex-column align-items-center text-center">
                    {ctx.role === 'employer' ? 
                    <img src="http://cdn.onlinewebfonts.com/svg/img_542942.png" alt="Employer" className="rounded-circle" width="150"/> 
                    : 
                    <img src="https://cdn.onlinewebfonts.com/svg/download_38097.png" alt="Student" className="rounded-circle" width="150"/>
                    }
                    <div className="mt-3">
                      <h4>{user.firstName} {user.lastName} {user.patronymic}</h4>
                      <p className="text-secondary mb-1">{ctx.role === "employer" ? "Employer" : "Student"}</p>
                      {ctx.role === "student" ?
                        <>
                          <p className="text-secondary mb-1">
                            University: <b>{user.university}</b>
                          </p>
                          <p className="text-secondary mb-1">
                            Faculty: <b>{user.faculty}</b>
                          </p>
                          <p className="text-secondary mb-1">
                            Course: <b>{user.course}</b>
                          </p>
                        </>
                       :
                        <>
                          <p className="text-secondary mb-1">
                            Company: <b>{user.company.name}</b>
                          </p>
                          <img className="img-fluid w-50" src={user.company.path}/>

                          <button className="btn btn-primary mt-5" type="button" data-bs-toggle="offcanvas" 
                          data-bs-target="#advertisementResponses" aria-controls="advertisementResponses">Advertisement responses</button>

                          <div className="offcanvas offcanvas-start" tabIndex="-1" id="advertisementResponses"
                             aria-labelledby="advertisementResponsesLabel">
                              <div className="offcanvas-header">
                                <h5 className="offcanvas-title" id="advertisementResponsesLabel">Responses from students: </h5>
                                <button type="button" className="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                              </div>
                              <div className="offcanvas-body list-group">
                                {responses.map(r => <div className="list-group-item list-group-item-action text-start">
                                    <div className="d-flex w-100 justify-content-between">
                                      <h5 className="mb-1">{r.title}</h5>
                                      <small>{r.leftAt}</small>
                                    </div>
                                    <p className="badge bg-primary">{r.positionName}</p> 
                                    <span className="mx-4 badge bg-info">{r.workTime}</span> 
                                    <span className="badge bg-info">{r.employmentType}</span>
                                    <hr />
                                    <p className="mb-1 mt-3">{r.student.fullName}</p>
                                    <ul>
                                        <li><b>{r.student.university}</b></li>
                                        <li><b>{r.student.faculty}</b></li>
                                        <li><b>{r.student.course} Course</b></li>
                                    </ul>
                                    <button type="button" className="btn btn-success" onClick={(e) => onResumeDownload(e, r.student.id, r.student.fullName)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-download mx-1" viewBox="0 0 16 16">
                                            <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z"/>
                                            <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3z"/>
                                          </svg>
                                        Resume
                                    </button>

                                    <button type="button" className="btn btn-outline-danger mx-2" onClick={(e) => onDeleteFromResponses(e, r.advertisementId, r.student.id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                                          <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                                          <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
                                        </svg>
                                    </button>
                                  </div>)}
                              </div>
                            </div>
                        </>
                      }
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div className="col-md-8">
              <div className="card mb-3">
                <div className="card-body">
                  <div className="row ">
                    <div className="col-sm-3">
                      <h6 className="mb-0">Full Name</h6>
                    </div>
                    <div className="col-sm-9 text-secondary">
                      {user.firstName} {user.lastName} {user.patronymic}
                    </div>
                  </div>
                  <hr />

                      {isEmailUpdated && <div id="emailUpdateModal">
                    <div className="modal-dialog" role="document">
                      <div className="modal-content">
                        <div className="modal-body">
                          A message of email updating confirm has been sent to your email. Please, check it.
                        </div>
                      </div>
                    </div>
                  </div>}

                  <div className="row align-items-center">
                    <div className="col-sm-3">
                      <h6 className="mb-0">Email</h6>
                    </div>
                    <div className="col-sm-8 text-secondary">{user.email}</div>
                    <button onClick={onUpdateEmail} className="col-sm-1 btn btn-success">
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil" viewBox="0 0 16 16">
                        <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"/>
                      </svg>
                    </button>
                  </div>
                  <hr />
                  <div className="row align-items-center">
                    <div className="col-sm-3">
                      <h6 className="mb-0">Phone</h6>
                    </div>
                    <div className="col-sm-8 text-secondary">{user.phone}</div>
                    <button className="col-sm-1 btn btn-success" 
                    data-bs-toggle="collapse" data-bs-target="#phoneUpdateCollapse" aria-expanded="false" aria-controls="phoneUpdateCollapse">
                      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil" viewBox="0 0 16 16">
                        <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"/>
                      </svg>
                    </button>
                    
                    <form className="w-50 collapse" id="phoneUpdateCollapse" onSubmit={onUpdatePhone}>
                    <input className="form-control" type='text' ref={phoneRef}  />
                    <button type="submit" className="btn btn-sm btn-success mt-3">Update</button>
                    </form>
                  </div>
                  <hr />
                  {ctx.role === "student" && (
                    <>
                      <div className="row align-items-center">
                        <div className="col-sm-3">
                          <h6 className="mb-0">Resume</h6>
                        </div>
                        <div className="col-sm-9 text-secondary">
                          {user.resume !== null ? <div className="d-flex justify-content-between align-items-center">
                            <div>{user.resume.name}</div>

                            <div>
                              <button className="btn btn-sm btn-primary m-2" onClick={(e) => onResumeDownload(e, userId)}>
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" className="bi bi-download" viewBox="0 0 16 16">
                                  <path d="M.5 9.9a.5.5 0 0 1 .5.5v2.5a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1v-2.5a.5.5 0 0 1 1 0v2.5a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2v-2.5a.5.5 0 0 1 .5-.5z"/>
                                  <path d="M7.646 11.854a.5.5 0 0 0 .708 0l3-3a.5.5 0 0 0-.708-.708L8.5 10.293V1.5a.5.5 0 0 0-1 0v8.793L5.354 8.146a.5.5 0 1 0-.708.708l3 3z"/>
                                </svg>
                              </button>
                            <button className="btn btn-sm btn-danger m-2" onClick={onResumeDelete}>
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                              <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                              <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
                            </svg>
                            </button>
                            </div>
                            
                            </div>
                          : 
                          <form onSubmit={onResumeUpload}>
                            <input type='file' ref={resumeRef} className='form-control' />
                              <button className="btn btn-primary mt-3" type="submit">Upload</button>
                          </form>
                          }
                        </div>
                      </div>
                      <hr />
                    </>
                  )}

                  <div className="row mb-5">
                    {error.length > 0 && <div className="alert alert-danger p-2">{error}</div>}
                  </div>
                    {ctx.role === 'employer' && <h4 className="mb-3">Published advertisements:</h4>}
                    {ctx.role === 'student' && <h4 className="mb-3">Featured:</h4>}
                  <div className="list-group">
                    {ctx.role === 'employer' && user.postedAdvertisements !== undefined && 
                    user.postedAdvertisements.map(a => <div className="list-group-item list-group-item-action" aria-current="true">
                        <div className="d-flex w-100 justify-content-between">
                          <h5 className="mb-1">{a.title}</h5>
                          <small>{a.postedAt}</small>
                        </div>
                        <div className="d-flex w-100 justify-content-between">
                          <b className="d-block text-primary">{a.positionName}</b>
                          <div className="btn-group">
                            <NavLink to={`/advertisements/${a.id}`} className="btn btn-primary btn-sm">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-right" viewBox="0 0 16 16">
                                  <path fill-rule="evenodd" d="M1 8a.5.5 0 0 1 .5-.5h11.793l-3.147-3.146a.5.5 0 0 1 .708-.708l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L13.293 8.5H1.5A.5.5 0 0 1 1 8z"/>
                                </svg>
                            </NavLink>
                              <button type="button" className="btn btn-sm btn-success" 
                              data-bs-toggle="collapse" data-bs-target={`#collapse-${a.id}`} aria-expanded="false" aria-controls={`collapse-${a.id}`}>
                                  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil" viewBox="0 0 16 16">
                                    <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z"/>
                                  </svg>
                              </button>
                              <button className="btn btn-danger btn-sm" type="button" onClick={(e) => {
                              e.preventDefault();
                              onDeleteAdvertisement(a.id);
                            } }>
                              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                              </svg>
                              </button>
                          </div>
                        </div>
                        
                        <AdvertisementUpdateForm title={a.title} position={a.positionName} description={a.description} collapse={`collapse-${a.id}`}
                        salary={a.salary} worktime={a.workTime} employmentType={a.employmentType} id={a.id} />
                        </div>)}


                        {ctx.role === 'student' && user.featuredAdvertisements !== undefined && 
                        user.featuredAdvertisements.map(f => <div key={f.advertisementId} className="list-group-item list-group-item-action mb-3 pt-3">
                        <div className="d-flex w-100 justify-content-between">
                          <h5 className="mb-1">{f.advertisement.title}</h5>
                          <b>{f.advertisement.postedAt}</b>
                        </div>
                        <p className="mb-1">{f.advertisement.positionName}.</p>
                        <small className="badge bg-primary">{f.advertisement.companyName}</small>
                        <span className="badge bg-info mx-3 text-dark">{f.advertisement.workTime}</span>
                        <span className="badge bg-info text-dark">{f.advertisement.employmentType}</span>
                        <div className="text-end">
                        <NavLink to={`/advertisements/${f.advertisementId}`} className="btn btn-sm btn-outline-primary mx-3">
                            <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-arrow-right" viewBox="0 0 16 16">
                                <path fillRule="evenodd" d="M1 8a.5.5 0 0 1 .5-.5h11.793l-3.147-3.146a.5.5 0 0 1 .708-.708l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L13.293 8.5H1.5A.5.5 0 0 1 1 8z"/>
                            </svg>
                        </NavLink>
                        <button type="button" onClick={(e) => onDeleteFromFeatured(e, f.advertisementId)} className="btn btn-danger">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                              <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                              <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
                            </svg>
                        </button>
                        </div>
                    </div>)}
                    
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Profile;
