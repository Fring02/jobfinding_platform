export const loginReducer = (prev, action) => {
    let error = '';
    switch (action.type) {
        case 'EMAIL':
            let regex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
            error = (!action.payload.match(regex)) ? 'Email is in wrong format' : '';
            return {
                ...prev, error: error, email: action.payload
            };
        case 'PASSWORD':
            error = (action.payload.length === 0) ? 'Password is empty' : '';
            return {
                ...prev, error: error, password: action.payload
            };
        default:
            break;
    }
}