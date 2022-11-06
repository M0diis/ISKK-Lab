import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

import appState from "app/appState";
import config from 'app/config';
import backend from 'app/backend';

import { ReviewForList } from 'models/Entities';
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
function AdminPanel()
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
    
    const [postTitle, setPostTitle] = useState<string>();
    const [postContent, setPostContent] = useState<string>();

    const createPost = (title: string, content: string) => {
        
        const post = {
            title: title,
            content: content,
            FK_UserID: appState.userId
        };
        
        update(() => {
            backend.post(
                config.backendUrl + "/post/create",
                post
            ).then(resp => {
                update(() => location.state = "refresh");

                notifySuccess("Post has been deleted.");
            })
            .catch(err => {
                const msg = `Creation new post has failed.`;
                
                notifyFailure(msg);
            })
        });
    }

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        if (!postTitle || !postContent) {
            alert("Please make sure all the data fields are filled");
            return;
        }
        
        createPost(postTitle, postContent);
    };
    
    // Render component HTML
    return (
        <>
            <br/>
                <div className="container">
                    <div className="row align-items-center justify-content-center">
                        <div style={{ width: "30rem;" }}>
                            <ul className="list-group list-group-flush text-center">
                                <li className="list-group-item">
                                    <button style={{ width: "150px" }} className="btn btn-sm btn-success" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#post-collapse"
                                            aria-controls="post-collapse">
                                        Create a new post
                                    </button>
                                </li>
                                <li className="list-group-item">
                                    <button style={{ width: "150px" }} className="btn btn-sm btn-success" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#user-collapse"
                                            aria-controls="user-collapse">
                                        Create a new user
                                    </button>
                                </li>
                            </ul>
                        </div>

                        <div className="col-lg-8 col-xl-8">
                            <div className="collapse" id="post-collapse">
                                <div className="card-body p-4 p-md-5">
                                    <br/>
                                        <form onSubmit={ handleSubmit }>
                                            <div className="form-outline mb-4">
                                                <input type="text" id="post_title" name="post_title"
                                                       className="form-control"
                                                         onChange={e => setPostTitle(e.target.value)}/>
                                                <label className="form-label" htmlFor="post_title">Title</label>
                                            </div>

                                            <div className="form-outline mb-4">
                                                <input type="text" id="post_content" name="post_content"
                                                       className="form-control"
                                                       onChange={e => setPostContent(e.target.value)}/>
                                                <label className="form-label" htmlFor="post_content">Content</label>
                                            </div>

                                            <button style={{ width: "150px" }} className="btn btn-sm btn-success"
                                                    type="submit">Create
                                            </button>
                                        </form>
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-8 col-xl-8">
                            <div className="collapse" id="user-collapse">
                                <div className="card-body p-4 p-md-5">
                                    <br/>
                                        <form method="post" action="_">

                                            <div className="form-outline mb-4">
                                                <input type="text" id="user_name" name="user_name"
                                                       className="form-control"/>
                                                <label className="form-label" htmlFor="user_name">Name</label>
                                            </div>

                                            <div className="form-outline mb-4">
                                                <input type="email" id="user_email" name="user_email"
                                                       className="form-control"/>
                                                <label className="form-label" htmlFor="user_email">Email</label>
                                            </div>

                                            <div className="form-outline mb-4">
                                                <input type="password" id="user_password" name="user_password"
                                                       className="form-control"/>
                                                <label className="form-label" htmlFor="user_password">Password</label>
                                            </div>

                                            <div className="form-check">
                                                <label className="form-check-label" htmlFor="is_admin">Admin</label>
                                                <input className="form-check-input" type="checkbox" id="is_admin"
                                                       name="is_admin"/>
                                            </div>

                                            <br/>

                                                <button style={{ width: "150px" }} className="btn btn-sm btn-success" type="submit">Create</button>
                                        </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </>
    );
}

export default AdminPanel;