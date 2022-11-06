import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

import appState from "app/appState";
import config from 'app/config';
import backend from 'app/backend';

import { ReviewForList } from 'models/Entities';
import LoadingData from "../components/LoadingData";

/**
 * Component state.
 */
class State
{
    isInitialized: boolean = false;
    isLoading: boolean = false;
    isLoaded: boolean = false;

    reviews: ReviewForList[] = [];

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
function Reviews()
{
    // Get state container and state updater
    const [state, updateState] = useState(new State());

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
        backend.get<ReviewForList[]>(
            config.backendUrl + "/review/list"
        ).then(res =>
        {
            update(state =>
            {
                // Indicate loading finished successfully
                state.isLoading = false;
                state.isLoaded = true;

                // Store data loaded
                state.reviews = res.data;
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

    const reviews = state.reviews
        .map(review =>
        {
            return (
                <div className="card mb-2">
                    <div className="card-body">
                        <div className="row">
                            <div className="col-md-2 text-center">
                                <img src="https://image.ibb.co/jw55Ex/def_face.jpg" style={{ width: "80%" }} className="img img-rounded img-fluid" alt="Profile" />
                                <p className="text-muted text-center mb-0 px-md-2 small">{review.createdTimestamp}</p>
                            </div>
                            <div className="col-md-10">
                                <p className="float-left"><strong>{review.userName}</strong></p>
                                <div className="clearfix"></div>
                                <p>{review.data}</p>

                                {appState.userAdmin &&
                                    <form method="post" action="_">
                                        <button type="submit" className="btn btn-sm btn-danger btn-lg mb-1">Delete</button>
                                    </form>
                                }

                            </div>
                        </div>
                    </div>
                </div>
            );
        });


    // Render component HTML
    return (
        <>
            <br/>
            <div className="container">
                <div className="row d-flex justify-content-center align-items-center h-100">
                    <div className="col-lg-10 col-xl-10">
                        {appState.isLoggedIn.value &&
                            <>
                                <h2 className="h1-responsive font-weight-bold my-4">LEAVE A REVIEW</h2>
                                <p>
                                    Since you're logged in, you can leave a review for any of the services I provide. Please be as descriptive as possible.
                                </p>

                                <form className="px-md-2" method="post" action="_">

                                    <div className="form-outline mb-4">
                                        <input type="text" id="review" name="review" className="form-control md-textarea" />
                                        <label className="form-label">Your review</label>
                                    </div>

                                    <button type="submit" className="btn btn-sm btn-success btn-lg mb-1">Submit</button>
                                </form>

                                <p className="text-muted text-center mb-0 px-md-2 small">Other reviews are shown below.</p>

                                <br/>
                            </>
                        }

                        { LoadingData({ type: "reviews", appState: state, data: reviews }) }
                        

                    </div>
                    <br/>
                </div>
            </div>
        </>
    );
}

export default Reviews;