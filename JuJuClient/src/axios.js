// import axios from 'axios'
// const axios = require('axios');

// const ToApi = axios.create({
//     baseURL: 'http://localhost:12346/api/',
//     timeout: 20000 // 20s timeout
//   });
  
//   export default ToApi;

import _axios from "axios"

const axios = (baseURL) => {
    //建立一個自定義的axios
    //本機
    // const server = 'http://localhost'
    //指定通用連接
    const server = 'http://192.168.1.175'
    // const server = 'http://10.211.55.3'

    const instance = _axios.create({
            baseURL: baseURL ||  server +':12346/api/', //JSON-Server端口位置
            // timeout: 1000,
        });

     return instance;
}

export {axios};
export default axios();