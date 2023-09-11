import { Link } from "react-router-dom";

export const Navbar = () => {
  return (
    <nav className="navbar navbar-expand-md bg-light-grey p-0">
      <div className="collapse navbar-collapse" id="navbarNavDropdown">
        <ul className="navbar-nav" style={{width:'100%'}}>
          <li className="nav-item col border border-dark ">
            <Link to="/dashboard" className="nav-link isActive">
              <h5 className="text-center mt-2 mb-2">Főoldal</h5>
            </Link>
          </li>
          <li className="nav-item col border border-dark ">
            <Link to="/stocks" className="nav-link isActive">
              <h5 className="text-center mt-2 mb-2">Összes részvény</h5>
            </Link>
          </li>
          <li className="nav-item col border border-dark">
            <Link to="/mystock" className="nav-link isActive">
              <h5 className="text-center mt-2 mb-2">Saját részvények</h5>
            </Link>
          </li>
          <li className="nav-item col border border-dark">
            <Link to="/transactions" className="nav-link isActive">
              <h5 className="text-center mt-2 mb-2">Tranzakciók</h5>
            </Link>
          </li>
          <li className="nav-item col border border-dark">
            <Link to="/rank" className="nav-link isActive">
              <h5 className="text-center mt-2 mb-2">Ranglista</h5>
            </Link>
          </li>
        <li className="nav-item col border border-dark">
            <Link to="/myprofile" className="nav-link isActive">
              <h5 className="text-center mt-2 mb-2">Felhasználó</h5>
            </Link>
          </li>
        </ul>
      </div>
    </nav>
  );
};
