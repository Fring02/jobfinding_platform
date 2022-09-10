export const registrationReducer = (prev, action) => {
    let error = '';
    switch (action.type) {
        case 'FIRSTNAME':
            error = (action.payload.length === 0) ? 'Firstname is empty' : '';
            return {
                ...prev, error: error, firstName: action.payload
            };
        case 'LASTNAME':
            error = (action.payload.length === 0) ? 'Lastname is empty' : '';
            return {
                ...prev, error: error, lastName: action.payload
            };
        case 'PATRONYMIC':
            error = (action.payload.length === 0) ? 'Patronymic is empty' : '';
            return {
                ...prev, error: error, patronymic: action.payload
            };
        case 'EMAIL':
            let emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
            error = (!action.payload.match(emailRegex)) ? 'Email is in wrong format' : '';
            return {
                ...prev, error: error, email: action.payload
            };
        case 'PHONE':
            let phoneRegex = /^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$/;
            error = (!action.payload.match(phoneRegex)) ? 'Mobile phone is in wrong format' : '';
            return {
                ...prev, error: error, phone: action.payload
            };
        case 'PASSWORD':
            error = (action.payload.length === 0) ? 'Password is empty' : '';
            return {
                ...prev, error: error, password: action.payload
            };
        case 'REPEAT_PASSWORD':
            error = (action.payload !== prev.password) ? 'Please, repeat password' : '';
            return {
                ...prev, error: error, repeatPassword: action.payload
            };
        case 'COMPANY':
            error = (action.payload.length === 0) ? 'Company must be chosen' : '';
            return {
                ...prev, error: error, companyId: action.payload
            };
        case 'UNIVERSITY':
            error = (action.payload.length === 0) ? 'University is empty' : '';
            return {
                ...prev, error: error, university: action.payload
            };  
        case 'FACULTY':
            error = (action.payload.length === 0) ? 'Faculty is empty' : '';
            return {
                ...prev, error: error, faculty: action.payload
            };
        case 'COURSE':
            let course = parseInt(action.payload);
            error = (course === 0) ? 'Course is empty' : '';
            return {
                ...prev, error: error, course: course
            };      
        default:
            break;
    }
}