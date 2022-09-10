export async function addToFeatured(advertisementId, studentId, accessToken){
    let params = new URLSearchParams({advertisementId: advertisementId, studentId: studentId});
    let response = await fetch(`https://localhost:5001/api/v1/featured?${params}`, 
    {method: 'POST', credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}

export async function deleteFromFeatured(advertisementId, studentId, accessToken){
    let params = new URLSearchParams({advertisementId: advertisementId, studentId: studentId});
    let response = await fetch(`https://localhost:5001/api/v1/featured?${params}`, 
    {method: 'DELETE', credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}

export async function getFeatured(studentId, accessToken){
    let params = new URLSearchParams({studentId: studentId});
    let response = await fetch(`https://localhost:5001/api/v1/featured?${params}`, 
    {method: 'GET', credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return await response.json();
}

export async function checkFeatured(advertisementId, studentId, accessToken){
    let params = new URLSearchParams({studentId: studentId, advertisementId: advertisementId});
    let response = await fetch(`https://localhost:5001/api/v1/featured/check?${params}`, {headers: {'Authorization': `Bearer ${accessToken}`}});
    let responseText = await response.text();
    return responseText === 'true';
}