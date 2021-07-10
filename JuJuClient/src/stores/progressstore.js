import React, { createContext, useState} from "react";

export const StoreContext = createContext();

export const StoreProvider = ({ children }) => {  
    const [userAccount, setuserAccount] = useState("");
    const [userName, setuserName] = useState("");
    const [isLogin, setisLogin] = useState(false);
    
    const store ={
        userAccountState: [userAccount, setuserAccount],
        userNameState: [userName, setuserName],
        isLoginState: [isLogin, setisLogin]
    };
    return (
        <StoreContext.Provider value={store}>
           {children}
        </StoreContext.Provider>
       );
};