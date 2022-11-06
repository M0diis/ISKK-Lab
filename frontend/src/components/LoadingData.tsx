
function LoadingData(props: any) {
    if(props.appState.isInitialized && !props.appState.isLoading && !props.appState.isLoaded) 
    {
        return (
            <div className="d-flex flex-column h-100 justify-content-center align-items-center">
                <span className="alert mx-2 text-muted">Failed to load data from backend.</span>
            </div>
        );
    }
    
    // Render component HTML
    if(props.appState.isLoading)
    {
        return (
            <div className="d-flex flex-column h-100 justify-content-center align-items-center">
                <span className="alert mx-2 text-muted">Loading { props.type ? props.type : 'data'}...</span>
                <i className="fas fa-spinner fa-spin fa-1x"></i>
            </div>
        );
    }
    
    return props.data;
}

export default LoadingData;