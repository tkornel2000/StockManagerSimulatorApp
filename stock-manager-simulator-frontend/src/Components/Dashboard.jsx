import React, { useContext, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import AuthContext from '../Context/AuthContext'

export const Dashboard = () => {
  const {auth} = useContext(AuthContext);

  const navigate = useNavigate();
  
  useEffect(() => {
    if (auth?.accessToken === undefined) {
      console.log("benta");
      navigate("/");
    }
  }, [auth, navigate]);

  return (
    <div>Dashboard</div>
  )
}