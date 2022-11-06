import axios from 'axios';

import config from 'app/config';
import appState from 'app/appState';
import backend, { replaceBackend } from 'app/backend';


/**
 * Log-out section in nav bar. React component.
 * @returns Component HTML.
 */
function StatusAndLogOut() {
	/**
	 * Handles 'Log-out' command.
	 */
	const onLogOut = () => {
		// Send log-out request to the backend
		backend.get(
			config.backendUrl + "/auth/logout",
			{
				params: {	
					jwt: appState.authJwt
				}
			}
		).then(res => {			 
			// Forget user information and JWT
			appState.userId = -1;
			appState.userTitle = "";
			appState.authJwt = "";
			appState.userAdmin = false;

			// Replace backend connector with axios instance having default configuration
			let defaultBackend = axios.create();
			replaceBackend(defaultBackend);

			// Indicate user is logged out
			appState.isLoggedIn.value = false;
		})
		// Login failed or backend error, show error message
		.catch(err => { });
	}

	// Render component HTML
	return (
		<>
			<span className="d-flex align-items-center">
				<span>Welcome, {appState.userTitle}</span>
				<button
					type="button"
					className="btn btn-primary btn-sm ms-2"
					onClick={() => onLogOut()}
				>Log out</button>
			</span>
		</>
	);
}

export default StatusAndLogOut;