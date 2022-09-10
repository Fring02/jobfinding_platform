export async function getResponsesByCompany(companyId, accessToken){
    let response = await fetch(`https://localhost:5001/api/v1/responses?companyId=${companyId}`,
     {headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return await response.json();
}

export async function checkResponse(advertisementId, studentId, accessToken){
    let response = await fetch(`https://localhost:5001/api/v1/responses/check?advertisementId=${advertisementId}&studentId=${studentId}`,
     {headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    let responseText = await response.text();
    return responseText === 'true';
}

export async function deleteResponse(advertisementId, studentId, accessToken){
    let response = await fetch(`https://localhost:5001/api/v1/responses?advertisementId=${advertisementId}&studentId=${studentId}`,
     {method: 'DELETE' ,headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}

export async function createResponse(advertisementId, studentId, letter, accessToken){
    let response = await fetch(`https://localhost:5001/api/v1/responses`,
     {method: 'POST', body: JSON.stringify(
        {advertisementId: advertisementId, studentId: studentId, coverLetter: letter}), 
        headers: {'Authorization': `Bearer ${accessToken}`,'Content-Type': 'application/json', 'Accept': 'application/json'}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}