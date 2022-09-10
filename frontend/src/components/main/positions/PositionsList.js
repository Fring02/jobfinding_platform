import React, { Fragment, useCallback, useEffect, useState } from "react";
import { getPositions } from "../../../api/positions_api";
import {NavLink} from 'react-router-dom';
import './PositionsList.css';
const PositionsList = () => {
    const [error, setError] = useState({
      isError: false, message: ''
    });

    const [positions, setPositions] = useState([]);

    const fetchPositions = useCallback(async () => {
      try {
        return await getPositions(1);
      } catch (e) {
        setError({isError: true, message: e.message});
      }
    }, []);

    useEffect(() => {
      fetchPositions().then(positions => setPositions(positions.data))
    }, [fetchPositions]);

    return <>
    <h1 className="text-center text-light">Available job positions: </h1>
    {error.isError && <div className="alert alert-danger text-center mt-4">{error.message}</div>}
    <div className="positions">
      {!error.isError && positions.length > 0 && 
        positions.map(c => 
        <div className="card position" key={c.id}>
          <img src={c.path} className="card-img-top" alt="..." />
          <div className="card-body positionBody">
            <h5 className="card-title">{c.name}</h5>
            <p className="card-text">Number of advertisements: {c.amount}</p>
            <NavLink to={`/advertisements?positionId=${c.id}`} className="btn btn-primary">Show advertisements...</NavLink>
          </div>
        </div>)}
      
    </div>
    </>;
}

export default PositionsList;