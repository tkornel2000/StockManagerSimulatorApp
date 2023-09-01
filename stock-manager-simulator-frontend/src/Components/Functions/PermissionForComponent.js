import { useNavigate } from "react-router-dom";
import { useEffect, useContext } from "react";
import AuthContext from "../../Context/AuthContext";

export const PermissionForComponent = () => {
    const {auth} = useContext(AuthContext);
    const navigate = useNavigate();  
    useEffect(() => {
      if (auth?.accessToken === undefined) {
        navigate("/");
      }
    }, [auth, navigate]);
}
