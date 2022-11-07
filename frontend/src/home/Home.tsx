import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

import appState from 'app/appState';
import config from 'app/config';
import backend from 'app/backend';

import { PostForList } from 'models/Entities';
import LoadingData from "../components/LoadingData";
import {notifyFailure, notifySuccess} from "../app/notify";

/**
 * Component state.
 */
class State
{
    isInitialized: boolean = false;
    isLoading: boolean = false;
    isLoaded: boolean = false;

    posts: PostForList[] = [];

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
function Home()
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

    const onDelete = (id: any) => {
        update(() => {
            backend.get(
                config.backendUrl + "/post/delete",
                {
                    params : {
                        id : id
                    }
                }
            ).then((res) => {
                update(() => location.state = "refresh");
                
                notifySuccess("Post has been deleted.");
            })
            .catch((err) => {
                //notify about operation failure
                const msg =
                    `Deletion of entity '${id}' has failed. ` +
                    `either entity is not deletable or there was backend failure.`;
                notifyFailure(msg);
            })
        });
    }
    
    // (re)initialize
    if (!state.isInitialized || location.state === "refresh")
    {
        // Query data
        backend.get<PostForList[]>(
            config.backendUrl + "/post/list"
        ).then(res =>
        {
            console.log(res);

            update(state =>
            {
                // Indicate loading finished successfully
                state.isLoading = false;
                state.isLoaded = true;

                // Store data loaded
                state.posts = res.data;
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

    const posts = state.posts
        .map(post =>
        {
            return (
                <div className="mb-2">
                    <hr></hr>
                    <div className="card-body p-2 p-sm-3">
                        <div className="media forum-item">
                            <img src="https://image.ibb.co/jw55Ex/def_face.jpg" className="mr-3 rounded-circle" width="50" alt="User" />
                            <div className="media-body">
                                <h6><p>{ post.userName }</p></h6>
                                <p className="text-secondary">
                                    { post.content }
                                </p>
                                <p className="text-muted">{ post.createdTimestamp }</p>

                                {appState.userAdmin &&
                                    <button type="button" onClick={() => onDelete(post.id)} className="btn btn-sm btn-danger">Delete</button>
                                    
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
                <br/>
                        <div className="row d-flex justify-content-center align-items-center h-100">

                            <div className="col-lg-8 col-xl-8 text-center">
                                <div className="card-body">
                                    <h1 className="card-title mb-1">Hi!</h1>
                                    <p className="card-text"><em>Welcome to my portfolio page. Here you will be able to find out a little about me and what tools and software I use.</em></p>
                                    <p className="card-text"><em>You can also communicate with me by registering and opening a support ticket, where we can discuss in more detail!</em></p>
                                </div>

                                <p className="card-text"><small className="text-muted">You can find some latest posts down below</small></p>
                            </div>

                            <div className="p-5 p-md-5"></div>

                            <div className="col-lg-8 col-xl-8">

                            { LoadingData({ type: "posts", appState: state, data: posts }) }

                            </div>
                        </div>
                </div>
        </>
    );
}

export default Home;