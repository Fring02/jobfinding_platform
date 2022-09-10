import { useCallback, useContext, useRef } from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import './AdvertisementPage.css';
import {getAdvertisementById} from '../../../api/advertisements_api';
import { addToFeatured, checkFeatured } from '../../../api/featured_api';
import AuthContext from '../../../store/auth_context';
import {checkResponse, createResponse} from '../../../api/responses_api';
import jwtDecode from 'jwt-decode';
export default function AdvertisementPage() {
    const [advertisement, setAdvertisement] = useState(null);
    const [error, setError] = useState('');
    const [isFeatured, setIsFeatured] = useState(false);
    const [isResponded, setIsResponded] = useState(false);
    const letterRef = useRef();
    const ctx = useContext(AuthContext);
    useEffect(() => {
        let id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
        getAdvertisementById(id).then(a => setAdvertisement(a)).catch(e => setError(e.message));
        if(ctx !== undefined){
            let userId = jwtDecode(ctx.accessToken)[
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            ];
            if(ctx.role === 'student'){
                checkFeatured(id, userId, ctx.accessToken).then(ok => {
                    setIsFeatured(ok);
                }).catch(e => setError(e.message));
                checkResponse(id, userId, ctx.accessToken).then(ok => setIsResponded(ok)).catch(e => setError(e.message));
            }
        }
    }, []);


    const onAddToFeatured = (e) => {
        e.preventDefault();
        let userId = jwtDecode(ctx.accessToken)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        addToFeatured(advertisement.id, userId, ctx.accessToken).then(_ => setIsFeatured(true)).catch(e => setError(e.message));
    }

    const onSubmitResponse = (e) => {
        e.preventDefault();
        let userId = jwtDecode(ctx.accessToken)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        createResponse(advertisement.id, userId, letterRef.current.value, ctx.accessToken).then(_ => setIsResponded(true))
        .catch(e => setError(e.message));
    }

    return <div className="row w-75 m-auto p-5 bg-light">
        {advertisement === null ? <div className='alert alert-danger'>{error}</div> :
        <><div className="col-md-9">
            <h1>{advertisement.title}</h1>
            <b className="text-success"><h3>{advertisement.salary} KZT</h3></b> <br />
            <ul className="h6">
                <li>{advertisement.employer.company.address}</li>
                <li>{advertisement.workTime}</li>
                <li>{advertisement.employmentType}</li>
            </ul>
            <div>
                {ctx.role === 'student' && <>
                {isFeatured === true ? <span className='badge bg-danger'>In featured</span> :
                <button type="button" onClick={onAddToFeatured} className="btn btn-large btn-outline-danger">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-heart" viewBox="0 0 16 16">
                    <path d="m8 2.748-.717-.737C5.6.281 2.514.878 1.4 3.053c-.523 1.023-.641 2.5.314 4.385.92 1.815 2.834 3.989 6.286 6.357 3.452-2.368 5.365-4.542 6.286-6.357.955-1.886.838-3.362.314-4.385C13.486.878 10.4.28 8.717 2.01L8 2.748zM8 15C-7.333 4.868 3.279-3.04 7.824 1.143c.06.055.119.112.176.171a3.12 3.12 0 0 1 .176-.17C12.72-3.042 23.333 4.867 8 15z"/>
                  </svg>
                </button>}
                
                {isResponded === true ? <span className='badge bg-success'>Submitted</span> : <>
                <button type="button" className="btn btn-large btn-success m-2" data-bs-toggle="collapse" 
                data-bs-target="#responseCollapse" aria-expanded="false" aria-controls="responseCollapse">
                  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                      <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                      <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                    </svg>
                </button>

                    <div className="collapse mt-4" id="responseCollapse">
                    <form onSubmit={onSubmitResponse}>
                        <textarea ref={letterRef} className='form-control' rows={5} placeholder='Attach here your cover letter (briefly explain why you are the candidate for this advertisement)'>
                        </textarea> <br />
                        <button type='submit' className='btn btn-primary'>Submit</button>
                    </form>
                </div></>}
                </>}
                
            </div>
            <hr />
            <p className="lead">{advertisement.description}</p>
        </div>
        <div className="col-md-3 text-center d-flex flex-column justify-content-center">
            <div>
                <img className="img-fluid w-25" src={advertisement.employer.company.path}/> <br />
                <h2 className="mt-2">{advertisement.employer.company.name}</h2>
                <p><i>{advertisement.employer.company.address}</i></p>
            </div>
            <div className="mt-4 d-flex">
                <div className="card employer-card">
                    <img src='https://static.thenounproject.com/png/1050475-200.png' className="card-img-top" alt="..." />
                    <div className="card-body text-center">
                      <h5 className="card-title">{advertisement.employer.firstName} {advertisement.employer.lastName} {advertisement.employer.patronymic}</h5>
                      <p className="card-text text-primary">{advertisement.employer.email}</p>
                    </div>
                  </div>
            </div>
        </div></>}
    </div>;
}