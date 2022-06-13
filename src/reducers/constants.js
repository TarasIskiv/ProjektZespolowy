const mainTypes = {
    switchTab: 'MAIN/SWITCH_TAB',
    setError: 'MAIN/SET_ERROR',
    setAuth: 'MAIN/SET_AUTH',
    setRole: 'MAIN/SET_ROLE',
    setErrorText: 'MAIN/SET_ERROR_TEXT',
    clear: 'MAIN/CLEAR'
};

const companyTypes = {
    setProfile: 'COMPANY/SET_PROFILE',
    setJobs: 'COMPANY/SET_JOBS',
    clear: 'COMPANY/CLEAR'
}

const jobTypes = {
    setJob: 'JOB/SET_JOB',
    setActiveJob: 'JOB/SET_ACTIVE_JOB'
}

const employeeTypes = {
    setEmployee: 'JOB/SET_EMPLOYEE',
    setActiveEmployee: 'JOB/SET_ACTIVE_EMPLOYEE'
}

export { mainTypes, companyTypes, jobTypes, employeeTypes }