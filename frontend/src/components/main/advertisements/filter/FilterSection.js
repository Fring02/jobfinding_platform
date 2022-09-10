import React, { useCallback, useContext, useEffect, useRef, useState } from "react";
import {getCompanies} from '../../../../api/companies_api';
import {getPositions} from '../../../../api/positions_api';
import FilterContext from '../../../../store/filter_context';
const FilterSection = (props) => {
  const ctx = useContext(FilterContext);
  const [filterView, setFilterView] = useState({isError: false, message: '', companies: [], positions: [], salaryRange: []});
  const filterRef = { valueRef: useRef(), positionIdRef: useRef(), companyIdRef: useRef(), 
    salaryRef: useRef(), workTimeRef: useRef(), employmentTypeRef: useRef() }

  const fetchFilters = useCallback(async () => {
    try{
      return await Promise.all([
        getCompanies(), getPositions()
      ]);
    } catch (e) {
      setFilterView({isError: true, message: e.message});
    }
  }, []);

  useEffect(() => {
    fetchFilters().then(([companies, positions]) => {
      let rangeLength = (props.salary % 50000 === 0) ? props.salary / 50000 : Math.floor(props.salary / 50000);
      let range = new Array(rangeLength);
      for(let i = 0, salary = 0; i < rangeLength; i++, salary += 50000){
          range.push(`${salary} - ${salary + 50000}`);
      }
      setFilterView({isError: false, message: '', companies: companies.data, positions: positions.data, salaryRange: range});
    });
  }, [fetchFilters]);


  const onSetFilter = (e) => {
    e.preventDefault();
    let salary = filterRef.salaryRef.current.value;
    let boundIndex = String(salary).indexOf('-');
    ctx.onSetFilter({salaryLeft: parseInt(String(salary).substring(0, boundIndex)), salaryRight: parseInt(String(salary).substring(boundIndex + 1)), 
      positionId: filterRef.positionIdRef.current.value, companyId: filterRef.companyIdRef.current.value, 
      workTime: filterRef.workTimeRef.current.value, employmentType: filterRef.employmentTypeRef.current.value,
      value: filterRef.valueRef.current.value});
  }
  const resetFilter = (e) => {
    e.preventDefault();
    ctx.onSetFilter({salaryLeft: 0, salaryRight: 0, positionId: '', companyId: '', workTime: '', employmentType: '', value: ''});
  }

    return <aside className="col-lg-3 text-light text-center">
    {filterView.isError && <div className="alert alert-danger text-center">{filterView.message}</div>}
    {!filterView.isError && 
    <form>
        <div className="input-group mb-3 col-md-11">
          <input type="search" id="form1" className="form-control" ref={filterRef.valueRef} placeholder="Search for advertisement..." />
          <button onClick={onSetFilter} type="submit" className="btn btn-primary">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-search" viewBox="0 0 16 16">
              <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z"/>
            </svg>
          </button>
          
          <button type="button" onClick={resetFilter} className="btn btn-success">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-clockwise" viewBox="0 0 16 16">
                  <path fill-rule="evenodd" d="M8 3a5 5 0 1 0 4.546 2.914.5.5 0 0 1 .908-.417A6 6 0 1 1 8 2v1z"/>
                  <path d="M8 4.466V.534a.25.25 0 0 1 .41-.192l2.36 1.966c.12.1.12.284 0 .384L8.41 4.658A.25.25 0 0 1 8 4.466z"/>
                </svg>
            </button>
        </div>
        
        <div className="mb-3">
            <label htmlFor="positionSelect" className="mb-3">Choose position:</label>
            <select onChange={onSetFilter} className="form-select form-select-lg mb-3" id="positionSelect" ref={filterRef.positionIdRef} 
            aria-label=".form-select-lg example">
              <option selected value={''}>Choose position...</option>
            {filterView.positions.map((p) =><option key={p.id} value={p.id}>{p.name}</option>)}
            </select>
        </div>

        <div className="mb-3">
            <label htmlFor="companySelect" className="mb-3">Choose company:</label>
            <select onChange={onSetFilter} className="form-select form-select-lg mb-3" id="companySelect" ref={filterRef.companyIdRef} 
            aria-label=".form-select-lg example">
            <option selected value={''}>Choose company...</option>
              {filterView.companies.map((c) => <option key={c.id} value={c.id}>{c.name}</option>)}
            </select>
        </div>
        
        <div className="mb-3">
            <label htmlFor="workTimeSelect" className="mb-3">Choose work schedule:</label>
            <select onChange={onSetFilter} className="form-select form-select-lg mb-3" id="workTimeSelect" ref={filterRef.workTimeRef} aria-label=".form-select-lg example">
              <option selected value=''>...</option>
              <option value='Undefined'>Not important</option>
              <option value='FullTime'>Full-time</option>
              <option value='PartTime'>Part-time</option>
              <option value='Individual'>Individual</option>
            </select>
        </div>
        
        <div className="mb-3">
            <label htmlFor="employmentTypeSelect" className="mb-3">Choose employment type:</label>
            <select onChange={onSetFilter} className="form-select form-select-lg mb-3" id="employmentTypeSelect" ref={filterRef.employmentTypeRef} aria-label=".form-select-lg example">
              <option selected value=''>...</option>
              <option value='Undefined'>Not important</option>
              <option value='Office'>Office</option>
              <option value='Remote'>Remote</option>
            </select>
        </div>
        
        <div className="mb-3">
          <label htmlFor="salarySelect" className="mb-3">Choose salary range:</label>
          <select onChange={onSetFilter} className="form-select form-select-lg mb-3" ref={filterRef.salaryRef} id="salarySelect" aria-label=".form-select-lg example">
              <option selected value={''}>Choose desired salary...</option>
            {filterView.salaryRange.map((s) => <option key={s} value={s}>{s}</option>)}
          </select>
        </div>
      </form>}
    
 </aside>;
}
export default FilterSection;