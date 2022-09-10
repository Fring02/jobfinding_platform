import React, { Fragment, useContext } from 'react';
import { NavLink } from 'react-router-dom';
import './Header.css';
import AuthContext from '../../store/auth_context';
const Header = () => {
  const ctx = useContext(AuthContext);

    return <nav className="navbar navbar-expand-lg bg-light mb-5">
    <div className="container-fluid">
        <NavLink className="navbar-brand" to='/'><img id='headerLogo' 
        src="https://upload.wikimedia.org/wikipedia/commons/d/db/Zeronet_logo.png"
          className="img-fluid" alt="" />
          </NavLink>
      <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span className="navbar-toggler-icon"></span>
      </button>
      <div className="collapse navbar-collapse" id="navbarSupportedContent">
        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
          <li className="nav-item">
            <NavLink className="nav-link" to='/advertisements'>Show advertisements</NavLink>
          </li>
          <li className="nav-item">
            <NavLink className="nav-link" to='/companies'>Companies</NavLink>
          </li>
          <li className="nav-item">
            <NavLink className="nav-link" to='/positions'>Positions</NavLink>
          </li>
        </ul>
        {!ctx.isAuthenticated && 
        <div className='btn-group' role='group' aria-label='SignGroup'>
          <NavLink className="nav-link" to='/signup'><button className='btn btn-success'>Sign up</button></NavLink>
          <NavLink className="nav-link" to='/signin'><button className='btn btn-primary'>Sign in</button></NavLink>
        </div>}
        {ctx.isAuthenticated && <div className='btn-group align-items-center' role='group' aria-label='AuthenticatedGroup'>
        {ctx.role === 'employer' && <NavLink to='/publish' className='btn btn-outline-primary'>Publish advertisement</NavLink>}
          <NavLink to='/chat' role='button' className='btn btn-outline-info'>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-chat" viewBox="0 0 16 16">
              <path d="M2.678 11.894a1 1 0 0 1 .287.801 10.97 10.97 0 0 1-.398 2c1.395-.323 2.247-.697 2.634-.893a1 1 0 0 1 .71-.074A8.06 8.06 0 0 0 8 14c3.996 0 7-2.807 7-6 0-3.192-3.004-6-7-6S1 4.808 1 8c0 1.468.617 2.83 1.678 3.894zm-.493 3.905a21.682 21.682 0 0 1-.713.129c-.2.032-.352-.176-.273-.362a9.68 9.68 0 0 0 .244-.637l.003-.01c.248-.72.45-1.548.524-2.319C.743 11.37 0 9.76 0 8c0-3.866 3.582-7 8-7s8 3.134 8 7-3.582 7-8 7a9.06 9.06 0 0 1-2.347-.306c-.52.263-1.639.742-3.468 1.105z"/>
            </svg>
          </NavLink>
          <NavLink to='/profile' role='button' className="btn btn-outline-dark">
            <svg xmlns="http://www.w3.org/2000/svg" width="30" height="25" fill="currentColor" className="bi bi-person" viewBox="0 0 16 16">
              <path d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0zm4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10z"/>
            </svg>
          </NavLink>
          <button className='btn btn-outline-danger' id='logoutBtn' type='button' onClick={ctx.onLogout}>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-door-closed" viewBox="0 0 16 16">
                <path d="M3 2a1 1 0 0 1 1-1h8a1 1 0 0 1 1 1v13h1.5a.5.5 0 0 1 0 1h-13a.5.5 0 0 1 0-1H3V2zm1 13h8V2H4v13z"/>
                <path d="M9 9a1 1 0 1 0 2 0 1 1 0 0 0-2 0z"/>
            </svg>
          </button>
        </div>}
        
      </div>
    </div>
  </nav>;
}

export default Header;