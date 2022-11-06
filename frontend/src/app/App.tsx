import { useRef, useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

import { Toast } from 'primereact/toast';

import appState from './appState';

import './App.scss';

import NavMenu from '../navmenu/NavMenu';
import Footer from 'footer/Footer';
import About from 'about/About';
import Home from 'home/Home';
import Contact from 'contact/Contact';
import Tools from 'tools/Tools';

import EntityCrud from 'entityCrud/EntityCrud';
import Reviews from 'reviews/Reviews';
import Tickets from 'tickets/Tickets';
import AdminPanel from "../admin-panel/AdminPanel";


class State {
	isInitialized : boolean = false;

	/**
	 * Makes a shallow clone. Use this to return new state instance from state updates.
	 * @returns A shallow clone of this instance.
	 */
	shallowClone() : State {
		return Object.assign(new State(), this);
	}
}

/**
 * Application. React component.
 * @returns Component HTML.
 */
function App() {
	//get state container and state updater
	const [state, setState] = useState(new State());

	//get ref to interact with the toast
	const toastRef = useRef<Toast>(null);

	/**
	 * This is used to update state without the need to return new state instance explicitly.
	 * It also allows updating state in one liners, i.e., 'update(state => state.xxx = yyy)'.
	 * @param updater State updater function.
	 */
	let update = (updater : () => void) => {
		updater();
		setState(state.shallowClone());
	}

	let updateState = (updater : (state : State) => void) => {
		setState(state => {
			updater(state);
			return state.shallowClone();
		})
	}

	//initialize
	if( !state.isInitialized )
	{
		//subscribe to app state changes
		appState.when(appState.isLoggedIn, () => {
			//this will force component re-rendering
			updateState(state => {});
		});

		//subscribe to user messages
		appState.msgs.subscribe(msg => {
			update(() => toastRef.current?.show(msg));
		});

		//indicate initialization is done
		updateState(state => state.isInitialized = true);
	}

	// Render component HTML
	let html =
		<Router>
			<link href="https://fonts.googleapis.com/css?family=Baloo+Paaji|Open+Sans:300,300i,400,400i,700,700i,800" rel="stylesheet"></link>
			<link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,700,300italic,400italic,700italic" rel="stylesheet"></link>
			<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"></link>
			<NavMenu/>
			<Toast ref={toastRef} position="top-right"/>
			<div className="shadow-sm bg-body rounded flex-grow-1 overflow-hidden p-1">
				<Routes>
					<Route path="/" element={<Home/>}/>
					<Route path="/home" element={<Home />} />
					{/* { appState.isLoggedIn.value && */}
						<>						

						<Route path="/about" element={<About/>}/>
						<Route path="/about-me" element={<About/>}/>
						<Route path="/contact" element={<Contact/>}/>
						<Route path="/contact-me" element={<Contact/>}/>
						<Route path="/tools" element={<Tools/>}/>
						<Route path="/reviews" element={<Reviews/>}/>
						<Route path="/tickets" element={<Tickets/>}/>
						<Route path="/admin-panel" element={<AdminPanel/>}/>
						<Route path="/entityCrud" element={<EntityCrud/>}/>
						</>
					{/* }					 */}
				</Routes>
				{/* { !appState.isLoggedIn.value &&
					<div className="d-flex flex-column h-100 justify-content-center align-items-center">
						<span className="alert alert-primary mx-2">Please, log in to see content.</span>
					</div>
				} */}
			</div>
			<Footer/>
		</Router>;
	
	//
	return html;
}

export default App;