export async function createAdvertisement(advertisement){
    let response = await fetch('https://localhost:5001/api/v1/advertisements', 
    {method: 'POST', credentials: 'include', body: JSON.stringify(advertisement), headers: {
        'Content-Type': 'application/json'
    }});
    if(!response.ok) throw new Error(await response.text());
    return response.json();
}

export async function getAdvertisements(page = 0, filter = null){
    let params = new URLSearchParams();
    if(page > 0) params.append("page", page);
    if(filter != null) {
        if(filter.positionId.length > 0) params.append('positionId', filter.positionId);
        if(filter.companyId.length > 0) params.append('companyId', filter.companyId);
        if(filter.salaryLeft > 0) params.append('salaryLeft', filter.salaryLeft);
        if(filter.salaryRight > 0) params.append('salaryRight', filter.salaryRight);
        if(filter.workTime.length > 0) params.append('workTime', filter.workTime);
        if(filter.employmentType.length > 0) params.append('employmentType', filter.employmentType);
        if(filter.value.length > 0) params.append('value', filter.value);
    }
    let response = await fetch(`https://localhost:5001/api/v1/advertisements?${params}`);
    if(!response.ok) throw new Error(await response.text());
    return response.json();
}

export async function getAdvertisementById(id) {
    let response = await fetch(`https://localhost:5001/api/v1/advertisements/${id}`);
    if(!response.ok) throw new Error(await response.text());
    return await response.json();
}

export async function deleteAdvertisement(id, accessToken){
    let response = await fetch(`https://localhost:5001/api/v1/advertisements/${id}`, 
    {method: 'DELETE', credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}

export async function updateAdvertisement(ad, id, accessToken){
    let response = await fetch(`https://localhost:5001/api/v1/advertisements/${id}`, 
    {method: 'PUT', body: JSON.stringify(ad), credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`, 
    'Content-Type': 'application/json'}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}