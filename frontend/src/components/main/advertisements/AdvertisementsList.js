import React, { Fragment, useCallback, useContext, useEffect, useState } from "react";
import AdvertisementsPagination from "./AdvertisementsPagination";
import FilterSection from "./filter/FilterSection";
import { getAdvertisements } from '../../../api/advertisements_api';
import FilterContext from "../../../store/filter_context";
import { NavLink } from "react-router-dom";
const AdvertisementsList = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const ctx = useContext(FilterContext);
  const [advertisementsData, setAdvertisementsData] = useState({results: [], salary: 0, count: 0});
  const onAdvertisementsChange = (page) => setCurrentPage(page);

  const fetchAdvertisements = useCallback(async (currentPage, filter) => await getAdvertisements(currentPage, filter), []);
  useEffect(() => {
    let filter = ctx.filter;
    const params = new Proxy(new URLSearchParams(window.location.search), {
      get: (searchParams, prop) => searchParams.get(prop),
    });
    let companyId = params.companyId;
    if(companyId !== null) filter.companyId = companyId;
    let positionId = params.positionId;
    if(positionId !== null) filter.positionId = positionId;
    
    let workTime = params.workTime;
    if(workTime !== null) filter.workTime = workTime;
    let employmentType = params.employmentType;
    if(employmentType !== null) filter.employmentType = employmentType;

    fetchAdvertisements(currentPage, filter).then(response => {
      setAdvertisementsData({results: response.data, salary: response.maxSalary, count: response.count});
    });
  }, [currentPage, ctx.filter]);

    return <><article className="col-lg-9">
      {(advertisementsData.results.length > 0) ? <Fragment>
    <div className="list-group">
      {advertisementsData.results.map(a =>
      <div key={a.id} className="list-group-item list-group-item-action mb-3 pt-3">
          <div className="d-flex w-100 justify-content-between">
            <h5 className="mb-1">{a.title}</h5>
            <b>{a.postedAt}</b>
          </div>
          <p className="mb-1">{a.positionName}.</p>
          <small className="badge bg-primary">{a.companyName}</small>
          <span className="badge bg-info mx-3 text-dark">{a.workTime}</span>
          <span className="badge bg-info text-dark">{a.employmentType}</span>
          <div className="text-end">
          <NavLink to={`/advertisements/${a.id}`} className="btn btn-sm btn-outline-primary">
              <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" className="bi bi-arrow-right" viewBox="0 0 16 16">
                  <path fillRule="evenodd" d="M1 8a.5.5 0 0 1 .5-.5h11.793l-3.147-3.146a.5.5 0 0 1 .708-.708l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L13.293 8.5H1.5A.5.5 0 0 1 1 8z"/>
              </svg>
          </NavLink>
          </div>
      </div>)
      }
    </div>
    <AdvertisementsPagination onPageChange={page => onAdvertisementsChange(page)} currentPage={currentPage} totalCount={advertisementsData.count} />
    </Fragment>
      : <div className="alert alert-danger">Found 0 advertisements.</div>}
  </article>
  
  <FilterSection salary={advertisementsData.salary} />
  </>;
}
export default AdvertisementsList;