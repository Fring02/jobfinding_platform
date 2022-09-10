import { useContext, useEffect, useRef, useState } from "react";
import {getPositions} from '../../api/positions_api';
import AuthContext from '../../store/auth_context';
import { updateAdvertisement } from "../../api/advertisements_api";
export default function AdvertisementUpdateForm(props) {
    const ctx = useContext(AuthContext);
    const workTimes = new Map().set("Full-time", "FullTime").set("Part-time", "PartTime")
    .set("Individual", "Individual").set("Not important", "Undefined");
    const employmentTypes = new Map().set("Office", "Office").set("Remote", "Remote").set("Not important", "Undefined");
    const [positions, setPositions] = useState([]);
    const [positionId, setPositionId] = useState();
    const [error, setError] = useState('');
    const ad = {
        titleRef: useRef(), salaryRef: useRef(), positionRef: useRef(),
        workTimeRef: useRef(), employmentTypeRef: useRef(), descriptionRef: useRef()
    };

    useEffect(() => {
        getPositions().then(p =>{setPositions(p.data); setPositionId(positions.find(p => p.name === props.position).id)});
    }, []);

    const onEditAdvertisement = (e) => {
        e.preventDefault();
        let advertisement = {title: ad.titleRef.current.value, salary: ad.salaryRef.current.value, 
            positionId: ad.positionRef.current.value, workTime: ad.workTimeRef.current.value, employmentType: ad.employmentTypeRef.current.value,
        description: ad.descriptionRef.current.value};
        updateAdvertisement(advertisement, props.id, ctx.accessToken).then(_ => window.location.href = '/').catch(e => setError(e.message));
    }


    return <form onSubmit={onEditAdvertisement} className="mt-3 p-3 collapse" id={props.collapse}>
        {error.length > 0 && <div className="alert alert-danger">{error}</div>}
        <div className="mb-3">
            <label htmlFor="title">Title</label>
            <input type='text' ref={ad.titleRef} value={props.title} className="form-control" />
        </div>
        <div className="mb-3">
            <label htmlFor="salary">Salary</label>
            <input type='number' ref={ad.salaryRef} min={0} value={props.salary} className="form-control" />
        </div>
        <div className="mb-3">
            <label htmlFor="position">Position</label>
            <select className="form-control" id="position" ref={ad.positionRef}>
                {positions !== undefined && <option value={positionId} selected>{props.position}</option>}
                {positions !== undefined && positions.filter(p => p.name !== props.position).map(p => <option value={p.id} selected>{p.name}</option>)}
            </select>
        </div>
        <div className="mb-3">
            <label htmlFor="worktime">Work schedule</label>
            <select className="form-control" ref={ad.workTimeRef}>
                <option value={workTimes.get(props.worktime)} selected>{props.worktime}</option>
                {Array.from(workTimes.keys()).filter(k => k !== props.worktime)
                .map(k => <option value={workTimes.get(k)}>{k}</option>)}
            </select>
        </div>
        <div className="mb-3">
            <label htmlFor="employmentType">Employment type</label>
            <select className="form-control" ref={ad.employmentTypeRef}>
                <option value={employmentTypes.get(props.employmentType)} selected>{props.employmentType}</option>
                {Array.from(employmentTypes.keys()).filter(k => k !== props.employmentType)
                .map(k => <option value={employmentTypes.get(k)}>{k}</option>)}
            </select>
        </div>
        <div className="mb-3">
            <label htmlFor="description">Description</label>
            <textarea rows={10} value={props.description} ref={ad.descriptionRef} className="form-control"></textarea>
        </div>
        <button type="submit" className="btn btn-success">Edit</button>
    </form>;
}