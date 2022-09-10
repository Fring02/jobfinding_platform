import React from "react";
import {NavLink} from 'react-router-dom';
export default function MainSection() {
    return <div className="row w-100">
            <div className="col-lg-6 text-center text-light">
                <h1 className="mb-5 display-4">Industrial student position hunters</h1>
                <p className="lead mb-5">
                    Industrial student position hunters (ISPH) is a online web service which is 
                    available for students and helps young people find their dream jobs. 
                </p>
                <div className="d-flex justify-content-center">
                    <NavLink to='/advertisements'
                     className="btn btn-lg btn-primary">Go to advertisements</NavLink>
                </div>
            </div>
            <div className="col-lg-2"></div>
            <img src="https://upload.wikimedia.org/wikipedia/commons/d/db/Zeronet_logo.png" 
            alt="" className="d-block img-fluid col-lg-4" />
        </div>;
}