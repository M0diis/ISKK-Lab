import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

import appState from "app/appState";
import config from 'app/config';
import backend from 'app/backend';

import { TicketForList } from 'models/Entities';

import $ from 'jquery';
import LoadingData from "../components/LoadingData";

/**
 * Component state.
 */
class State
{
    isInitialized: boolean = false;
    isLoading: boolean = false;
    isLoaded: boolean = false;

    tickets: TicketForList[] = [];

    isDeleting: boolean = false;

    /**
     * Makes a shallow clone. Use this to return new state instance from state updates.
     * @returns A shallow clone of this instance.
     */
    shallowClone(): State
    {
        return Object.assign(new State(), this);
    }
}

/**
 * Prints some information about the app. React component.
 * @returns Component HTML.
 */
function Tickets()
{
    //get state container and state updater
    const [state, updateState] = useState(new State());

    //get router stuff
    const navigate = useNavigate();
    const location = useLocation();

    /**
     * This is used to update state without the need to return new state instance explicitly.
     * It also allows updating state in one liners, i.e., 'update(state => state.xxx = yyy)'.
     * @param updater State updater function.
     */
    let update = (updater: (state: State) => void) =>
    {
        updateState(state =>
        {
            updater(state);
            return state.shallowClone();
        })
    }
    
    //(re)initialize
    if (!state.isInitialized || location.state === "refresh")
    {
        // Query data
        backend.get<TicketForList[]>(
            config.backendUrl + "/ticket/list/by-user",
            {
                params: {
                    userId: appState.userId
                }
            }
        ).then(res =>
        {
            console.log(res);

            update(state =>
            {
                // Indicate loading finished successfully
                state.isLoading = false;
                state.isLoaded = true;

                // Store data loaded
                state.tickets = res.data;
            })
        })

        // Drop location state to prevent infinite re-updating
        location.state = null;

        // Indicate data is loading and initialization done
        update(state =>
        {
            state.isLoading = true;
            state.isLoaded = false;
            state.isInitialized = true;
        });
    }

    const tickets = Array.from(state.tickets)
        .map(ticket =>
        {
            return (
                <>
                    <div id={ ticket.title } className="filterable">
                        <a className="list-group-item list-group-item-action flex-column align-items-start" href="/tickets/view/{{ ticket.id }}">
                            <div className="d-flex w-100 justify-content-between">
                                <h5 className="mb-1">{ ticket.title }</h5>
                                <small>{ ticket.createdTimestamp }</small>
                            </div>
                            <p className="mb-1">{ ticket.description }</p>
                            <small>{ ticket.closed ? 'Closed' : 'Open' }</small>
                        </a>
                    </div>
                    <br></br>
                </>
            );
        });

    $(() =>
    {
        $('#topic_search').on('input', (e) =>
        {
            const filterable = $('.filterable');

            const value = $(e.target).val()?.toString() || '';

            for (let i = 0; i < filterable.length; i++)
            {
                const el = filterable[i];
                const id = el.id.toLowerCase();

                el.style.display = value !== '' ? (id.includes(value) ? '' : 'none') : '';
            }
        });
    });
    
    // Render component HTML
    return (
        <>
            <br></br>
            <div className="container">
                <div className="row d-flex justify-content-center align-items-center h-100">
                    <div className="col-lg-8 col-xl-8">
                        <div className="card-body p-4 p-md-5">
                            <h2 className="h1-responsive font-weight-bold my-4">CREATE A TICKET</h2>
                            <p>
                                Create a ticket where we can discuss your project.
                            </p>
                            <p>
                                I will get back to you as soon as possible.
                            </p>

                            <form className="px-md-2" method="post" action="_">

                                <div className="form-outline mb-4">
                                    <input type="text" id="ticket_title" name="ticket_title" className="form-control" />
                                    <label className="form-label">Subject</label>
                                </div>

                                <div className="form-outline mb-4">
                                    <input type="text" id="ticket_description" name="ticket_description" className="form-control md-textarea" />
                                    <label className="form-label">Description</label>
                                </div>

                                <button type="submit" className="btn btn-sm btn-success btn-lg mb-1">Submit</button>
                            </form>

                            <br></br>

                            <input type="text" id="topic_search" name="topic_search" className="form-control" />
                            <label className="form-label">Search by ticket subject</label>

                            <p className="text-muted text-center mb-0 px-md-2 small">You can find your tickets below.</p>
                            <p className="text-muted text-center mb-0 px-md-2 small">To view more information, click on the ticket.</p>
                        </div>
                    </div>

                    <div className="col-md-6">
                        <div className="list-group">
                            { LoadingData({ type: "tickets" , appState: state, data: tickets }) }
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Tickets;