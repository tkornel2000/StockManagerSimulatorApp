import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from './Components/Login';
import { Dashboard } from './Components/Dashboard';
import Register from './Components/Register';
import { AuthProvider } from './Context/AuthContext';

function App() {

  return (
    <BrowserRouter>
    <AuthProvider>
      <div className='bg-img'>
      <Routes>
        <Route path='/' element={<Login/>}/>
        <Route path='/register' element={<Register/>}/>
        <Route path='/dashboard' element={<Dashboard/>}/>
      </Routes>
      </div>
      </AuthProvider>
    </BrowserRouter>
  );
}

export default App;
