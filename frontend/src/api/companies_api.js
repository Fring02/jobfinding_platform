export async function getCompanies(page = 0){
    let url = 'http://localhost:5000/api/v1/companies';
    if(page > 0) url += `?${new URLSearchParams({page: page})}`;
    let response = await fetch(url);
    if(!response.ok){
        throw new Error("Failed to load companies");
    }
    return await response.json()
}