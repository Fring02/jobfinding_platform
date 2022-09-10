import React, { Fragment, useCallback, useContext, useEffect, useState } from "react";
import { getCompanies } from "../../../api/companies_api";
import {NavLink} from 'react-router-dom';
import './CompaniesList.css';
const CompaniesList = () => {
    const [error, setError] = useState({
      isError: false, message: ''
    });
    const [companies, setCompanies] = useState([]);
    const fetchCompanies = useCallback(async () => {
      try {
        return await getCompanies();
      } catch (e) {
        setError({isError: true, message: e.message});
      }
    }, []);

    useEffect(() => {
      fetchCompanies().then(companies => setCompanies(companies.data))
    }, [fetchCompanies]);

    return <>
    <h1 className="text-center text-light">Companies on ISPH: </h1>
    {error.isError && <div className="alert alert-danger text-center mt-4">{error.message}</div>}
    <div className="companies">
      {!error.isError && companies.length > 0 && 
        companies.map(c => 
        <div className="card company" key={c.id}>
          <img src={c.path} className="card-img-top companyImg" alt="..." />
          <div className="card-body companyBody">
            <h5 className="card-title">{c.name}</h5>
            <NavLink to={`/advertisements?companyId=${c.id}`} className="btn btn-primary">Show advertisements...</NavLink>
          </div>
        </div>)}
      
    </div>
    </>;
}

export default CompaniesList;