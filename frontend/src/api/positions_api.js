export async function getPositions(page = 0){
    let url = 'https://localhost:5001/api/v1/positions';
    if(page > 0) url += `?${new URLSearchParams({page: page})}`;
    let response = await fetch(url);
    if(!response.ok){
        throw new Error("Failed to load positions");
    }
    return await response.json()
}