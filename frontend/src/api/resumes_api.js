import Cookies from "js-cookie";

export async function uploadResume(id, resume, accessToken = Cookies.get('jwt-access')){
    let data = new FormData();
    data.append('file', resume);
    let response = await fetch(`https://localhost:5001/api/v1/resumes/${id}`, 
    {method: 'POST', credentials: 'include', body: data, headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}

export async function downloadResume(id, accessToken = Cookies.get('jwt-access')){
    let response = await fetch(`https://localhost:5001/api/v1/resumes/${id}`, 
    {credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return await response.blob();
}

export async function deleteResume(id, accessToken = Cookies.get('jwt-access')){
    let response = await fetch(`https://localhost:5001/api/v1/resumes/${id}`, 
    {method: 'DELETE', credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}