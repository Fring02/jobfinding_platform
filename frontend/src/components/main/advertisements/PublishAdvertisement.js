import jwtDecode from "jwt-decode";
import { Fragment, useCallback, useContext, useEffect, useRef, useState } from "react";
import { useNavigate } from 'react-router-dom';
import { createAdvertisement } from "../../../api/advertisements_api";
import {getPositions} from '../../../api/positions_api';
import AuthContext from '../../../store/auth_context';
export default function PublishAdvertisement() {
    const [positions, setPositions] = useState([]);
    const [error, setError] = useState('');
    const ctx = useContext(AuthContext);
    const navigate = useNavigate();
    useEffect(() => {getPositions().then(p => setPositions(p.data)).catch(e => setError(e.message));}, []);
    const advertisementRef = {
        titleRef: useRef(),
        positionRef: useRef(),
        salaryRef: useRef(),
        descriptionRef: useRef(),
        workTimeRef: useRef(),
        employmentTypeRef: useRef()
    }
    const onCreateAdvertisement = (e) => {
        e.preventDefault();
        if(Object.values(advertisementRef).some(v => v.current.value.length === 0)){
            setError('Fill all fields'); return;
        }
        let advertisement = {
            title: advertisementRef.titleRef.current.value,
            positionId: advertisementRef.positionRef.current.value,
            salary: advertisementRef.salaryRef.current.value,
            description: advertisementRef.descriptionRef.current.value,
            employerId: jwtDecode(ctx.accessToken)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
            workTime: advertisementRef.workTimeRef.current.value,
            employmentType: advertisementRef.employmentTypeRef.current.value
        }
        createAdvertisement(advertisement).then(response => {
            navigate('/advertisements');
        }).catch(e => setError(e.message));
    }

  return (
    <div className="row container-fluid">
      <div className="col-md-1"></div>
      <div className="col-md-3">
        <img
          src="https://www.tlnt.com/wp-content/uploads/sites/4/2016/10/online-job-postings.jpg"
          className="img-fluid"/> <br />
        <p className="mt-5 text-light text-center">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever 
          since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five
          centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release
            of Letraset sheets containing
          Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
      </div>
      <div className="col-md-1"></div>
      <form onSubmit={onCreateAdvertisement} className="col-md-6 text-light">
        <h1 className="text-center">New advertisement</h1>
        {error.length !== 0 && <div className="alert alert-danger text-center">{error}</div>}
        <div className="mb-3">
          <label htmlFor="title" className="form-label">
            Title
          </label>
          <input type="text" className="form-control" id="title" ref={advertisementRef.titleRef} aria-describedby="title"/>
        </div>
        <div className="mb-3">
          <label htmlFor="position" className="form-label">
            Position
          </label>
          <select className="form-select" aria-label="position" ref={advertisementRef.positionRef}>
            {positions.map((p, id) => (id === 0) ? <option selected value={p.id}>{p.name}</option> : <option value={p.id}>{p.name}</option>)}
          </select>
        </div>
        
        <div className="mb-3">
          <label htmlFor="desc" className="form-label">Description</label>
          <textarea rows={4} className="form-control" id="desc" ref={advertisementRef.descriptionRef} aria-describedby="desc"></textarea>
        </div>
        <div className="mb-3">
          <label htmlFor="salary" className="form-label">Salary</label>
          <input type="number" min={0} className="form-control" id="salary" ref={advertisementRef.salaryRef} aria-describedby="salary"/>
        </div>
        <div className="mb-3">
          <label htmlFor="position" className="form-label">Work schedule</label>
          <select className="form-select" ref={advertisementRef.workTimeRef} aria-label="worktime">
            <option value='Undefined'>Not important</option>
            <option value='FullTime'>Full-time</option>
            <option value='PartTime'>Part-time</option>
            <option value='Individual'>Individual</option>
          </select>
        </div>
        <div className="mb-3">
          <label htmlFor="position" className="form-label">Employment type</label>
          <select className="form-select" ref={advertisementRef.employmentTypeRef} aria-label="employmenttype">
            <option value='Undefined'>Not important</option>
            <option value='Office'>Office</option>
            <option value='Remote'>Remote</option>
          </select>
        </div>
        <button type="submit" className="btn btn-primary btn-lg">
          Publish
        </button>
      </form>
      <div className="col-md-1"></div>
    </div>
  );
}
