
export async function register(data, role){
    let url = 'https://localhost:5001/api/v1/';
    if(role === 'employer'){
        url += 'employers/register';
    } else {
        url += 'students/register';
    }
    let response = await fetch(url, 
    {method: 'POST', body: JSON.stringify(data), credentials: 'include', withCredentials: true, 
    headers: {'Content-Type': 'application/json'}});
    if(!response.ok) throw new Error(await response.text());
    return await response.json();
}
export async function login(data, role){
    return (role === 'student') ? await loginStudent(data) : await loginEmployer(data);
}
async function loginEmployer(data){
    let url = 'http://localhost:5000/api/v1/employers/login';
    let response = await fetch(url, {method: 'POST', body: JSON.stringify(data), credentials: 'include', withCredentials: true, 
     headers: {'Content-Type': 'application/json'}});
    if(!response.ok) throw new Error(await response.text());
    return await response.json();
}
async function loginStudent(data){
    let url = 'https://localhost:5001/api/v1/students/login';
    let response = await fetch(url, {method: 'POST', body: JSON.stringify(data), credentials: 'include', 
    withCredentials: true, headers: {'Content-Type': 'application/json'}});
    if(!response.ok) throw new Error(await response.text());
    return await response.json();
}

export async function getUser(id, role, accessToken){
    if(role === 'employer'){
        let response = await fetch(`https://localhost:5001/api/v1/employers/${id}`, 
        {credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
        if(!response.ok) throw new Error(await response.text());
        return await response.json();
    } else if (role === 'student'){
        let response = await fetch(`https://localhost:5001/api/v1/students/${id}`, 
        {credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
        if(!response.ok) throw new Error(await response.text());
        return await response.json();
    } else return null;
}

export async function sendEmailUpdateMessage(id, role, accessToken) {
    let url = `https://localhost:5001/api/v1/${role}s/${id}/update_email`;
    let response = await fetch(url, {method: 'PUT', credentials: 'include', headers: {'Authorization': `Bearer ${accessToken}`}});
    if(!response.ok) throw new Error(await response.text());
    return await response.text();
}


export async function updateUser(id, role, data, accessToken){
    let url = `https://localhost:5001/api/v1/${role}s/${id}`;
    let response = await fetch(url, {method: 'PUT', credentials: 'include', body: JSON.stringify(data),
    headers: {'Authorization': `Bearer ${accessToken}`, 'Content-Type': 'application/json'}});
    if(!response.ok) throw new Error(await response.text());
    return response.ok;
}