import { NavLink } from "react-router-dom";

import Auth from 'auth/Auth';

import appState from "app/appState";

import './NavMenu.scss'

/**
 * Navigation menu. React component.
 * @returns Component HTML.
 */
function NavMenu() {
	// Render component HTML
	return (	
		<header>
			<nav className="navbar navbar-expand-xl shadow-sm bg-body rounded m-1 d-flex justify-content-between align-items-center" id="navid">
				<div className="container-fluid">
					<button className="navbar-toggler" type="button" data-bs-toggle="collapse"
							data-bs-target="#navbarSupportedContent"
							aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
						<span className="btn btn-sm btn-info">Navigation</span>
					</button>
					<div className="collapse navbar-collapse" id="navbarSupportedContent">
						<a className="navbar-brand" href="/home">Modestas Kaz. | Developer</a>
						<NavLink
							to="/"
							className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
						>HOME</NavLink>
						<NavLink
							to="/about"
							className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
						>ABOUT</NavLink>
						<NavLink
							to="/contact"
							className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
						>CONTACT ME</NavLink>
						<NavLink
							to="/tools"
							className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
						>TOOLS</NavLink>
						<NavLink
							to="/reviews"
							className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
						>REVIEWS</NavLink>
						{appState.isLoggedIn.value &&
							<>
								<NavLink
									to="/tickets"
									className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
								>TICKETS</NavLink>
								<NavLink
									to="/admin-panel"
									className={it => "nav-link mx-2 " + (it.isActive ? "active" : "")}
								>ADMIN-PANEL</NavLink>
							</>
						}
					</div>

					<span>
						<Auth/>
					</span>
				</div>
			</nav>
		</header>
	);
}

export default NavMenu;