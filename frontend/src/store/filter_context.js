import React, { useEffect, useState } from "react";
const FilterContext = React.createContext({
    filter: {salaryLeft: 0, salaryRight: 0, positionId: '', companyId: '', value: '', workTime: '', employmentType: ''},
    onSetFilter: () => {}
});

export const FilterContextProvider = (props) => {
    const [filter, setFilter] = useState({salary: 0, salaryLeft: 0, salaryRight: 0, 
        positionId: '', companyId: '', value: '', workTime: '', employmentType: ''});

    return <FilterContext.Provider value={{filter: filter, onSetFilter: setFilter}}>
                {props.children}
           </FilterContext.Provider>
}
export default FilterContext;