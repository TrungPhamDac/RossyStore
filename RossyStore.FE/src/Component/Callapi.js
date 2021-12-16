import axios from 'axios';
const apiBody = "http://localhost:44328/api"
const getAll = "GetAll"
export default function callApi (endpoint, method, data) {
    return axios({
        method: method,
        url: `${apiBody}/${endpoint}/${getAll}`,
        data: data
    }).catch(error=>{
        console.log(error);
    })
}