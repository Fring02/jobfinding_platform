import { useContext, useRef, useState } from "react";
import { useNavigate } from "react-router";
import { updateUser } from "../../api/users_api";
import jwtDecode from 'jwt-decode';
import AuthContext from '../../store/auth_context';
export default function EmailUpdateForm(){
    const ctx = useContext(AuthContext);
    const [error, setError] = useState('');
    const emailRef = useRef();
    const navigate = useNavigate();

    const onEmailUpdate = (e) => {
        e.preventDefault();
        const userId = jwtDecode(ctx.accessToken)[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ];
        updateUser(userId, ctx.role, {email: emailRef.current.value}, ctx.accessToken)
        .then(_ => navigate('/profile')).catch(e => setError(e.message));
    }

    return <form className="w-50 text-light m-auto" onSubmit={onEmailUpdate}>
    {error.length > 0 && <div className="alert alert-danger">{error}</div>}
    <div class="mb-3">
      <label for="email" class="form-label">New email</label>
      <input type="email" className="form-control" id="email" ref={emailRef} />
    </div>
    <button type="submit" className="btn btn-primary">Submit</button>
  </form>;
}