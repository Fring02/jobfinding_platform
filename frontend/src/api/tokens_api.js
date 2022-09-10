import Cookies from "js-cookie";
import jwtDecode from "jwt-decode"

export async function revokeToken(accessToken, refreshToken){
    let decodedToken = jwtDecode(accessToken);
    let role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    let response = await fetch(`https://localhost:5001/api/v1/${role}s/revoke`,
     {method: 'POST', credentials: 'include', headers: 
     {'Authorization': `Bearer ${accessToken}`, 'Access-Token': accessToken, 'Refresh-Token': refreshToken}});
    if(!response.ok) throw new Error(await response.text());
    else {
        Cookies.remove('jwt-access');
        Cookies.remove('jwt-refresh');
    }
}

export async function refreshToken(accessToken, refreshToken){
    let decodedToken = jwtDecode(accessToken);
    let role = decodedToken['role'];
    let response = await fetch(`https://localhost:5001/api/v1/${role}s/refresh`,
    {method: 'POST', credentials: 'include', headers: 
    {'Authorization': `Bearer ${accessToken}`, 'Access-Token': accessToken, 'Refresh-Token': refreshToken}});
    if(!response.ok) throw new Error(await response.text());
}