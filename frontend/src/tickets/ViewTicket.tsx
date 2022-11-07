import { useState } from 'react';
import {useLocation, useNavigate, useParams} from 'react-router-dom';

import appState from "app/appState";
import config from 'app/config';
import backend from 'app/backend';

import {TicketForList, TicketMessages} from 'models/Entities';

import $ from 'jquery';
import LoadingData from "../components/LoadingData";
import {EntityForCU} from "../entityCrud/models";

/**
 * Component state.
 */
class State
{
    isInitialized: boolean = false;
    isLoading: boolean = false;
    isLoaded: boolean = false;

    ticket: TicketForList | null = null;
    messages: TicketMessages[] = [];
    
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
function ViewTicket()
{
    // Get state container and state updater
    const [state, updateState] = useState(new State());
    
    const { ticketId } = useParams();

    // Get router stuff
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
    
    // (re)initialize
    if (!state.isInitialized || location.state === "refresh")
    {
        // Query data
        backend.get<TicketForList>(
            config.backendUrl + `/ticket/load`,
            {
                params : {
                    ticketId : ticketId
                }
            }
        ).then(res =>
        {
            console.log(res);

            update(state =>
            {
                // Store data loaded
                state.ticket = res.data;
            })
        });
        
        backend.get<TicketMessages[]>(
            config.backendUrl + `/ticket/messages`,
            {
                params : {
                    ticketId : ticketId
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
                state.messages = res.data;
            })
        });

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
    
    const ticketMessages = Array.from(state.messages).map((message) => {
       return (
           <>
               <a className="list-group-item list-group-item-action flex-column align-items-start">
                   <div className="d-flex w-100 justify-content-between">
                       <h5 className="mb-1">{ message.userName }</h5>
                   </div>
                   <p className="mb-1">{ message.content }</p>
                   <small>{ message.createdTimestamp }</small>

                   {appState.userAdmin &&
                       <form method="post" action="_">
                           <input type="hidden" id="ticket_id" name="ticket_id" value={ ticketId }/>

                           <div className="d-grid d-md-flex justify-content-md-end">
                               <button className="btn btn-sm btn-danger" type="submit">Delete</button>
                           </div>
                       </form>
                   }
               </a>
               <br/>
           </>
       )
    });
    
    // Render component HTML
    return (
        <>
            <br/>
            <div className="container">
                <div className="row d-flex justify-content-center align-items-center h-100">

                    <div className="col-lg-8">
                        <div className="card-body">
                            <h3 className="card-title mb-1">{ state.ticket?.title }</h3>
                            <p className="card-text"><em>{ state.ticket?.description}</em></p>

                            <p className="card-text"><small className="text-muted">{ state.ticket?.createdTimestamp }</small></p>
                        </div>
                    </div>

                    <div className="col-lg-8">
                        <div className="card-body p-4 p-md-5">
                            <form className="px-md-2" method="post" action="_">

                                <div className="form-outline mb-4">
                                    <label className="form-label" htmlFor="content">Message</label>
                                    <input type="text" id="content" name="content"
                                           className="form-control md-textarea"/>
                                </div>

                                <input type="hidden" id="ticket_id" name="ticket_id" value={ ticketId }/>

                                <button type="submit" className="btn btn-sm btn-success btn-lg mb-1">Send</button>
                            </form>

                            <form className="px-md-2" method="post" action="_">

                                <input type="hidden" id="ticket_id" name="ticket_id" value={ ticketId }/>
                                <button type="submit" className="btn btn-sm btn-danger btn-lg mb-1">Close</button>
                            </form>

                            <p className="text-muted text-center mb-0 px-md-2 small">You can find the previous converstation below.</p>

                        </div>
                    </div>

                    <div className="col-md-8">
                        <div className="list-group">
                            { LoadingData({ type: 'ticket messages', appState: state, data: ticketMessages })}
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default ViewTicket;