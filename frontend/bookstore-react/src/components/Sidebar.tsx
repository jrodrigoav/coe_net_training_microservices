import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import { routeItems } from '../routes/routeItems';


const Sidebar: React.FC = () => {

    const [expanded, setExpanded] = useState(false);

    const toggleSidebar = () => {
        setExpanded(!expanded);
      };

    return (
        <div className={`d-flex flex-column flex-shrink-0 p-3 bg-light h-100 ${expanded ? 'expanded' : ''}`}>
            <div className="d-flex gap-2 align-items-center justify-content-md-center link-dark text-decoration-none fs-4">
                <button type="button" className="navbar-toggler d-md-none" onClick={toggleSidebar}>
                    <i className="bi bi-list"></i>
                </button>
                <i className="bi bi-book"></i>
                <span>Bookstore</span>
            </div>
            <hr />
            <ul className="nav nav-pills flex-column mb-auto">
                {routeItems.map((item) => (
                    <li className="nav-item" key={item.path}>
                        <NavLink
                            to={item.path}
                            className={({ isActive }) =>
                                isActive ? 'active' : ''
                            }
                        >
                            <i className={item.icon}></i>
                            {item.title}
                        </NavLink>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Sidebar;