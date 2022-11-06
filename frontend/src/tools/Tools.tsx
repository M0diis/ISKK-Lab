/**
 * Prints some information about the app. React component.
 * @returns Component HTML.
 */
function Tools()
{
    // Render component HTML
    const html =
        <>
            <br/>
            <div style={{ height: "80%" }} className="container justify-content-center align-items-center">
                <div className="row">
                    <div className="col-md-12">
                        <div>
                            <div className="card-body">
                                <div className="card-title">
                                    <h3 className="text-center title-2">Tools & Software</h3>
                                </div>
                                <hr></hr>
                                <div>
                                    <p className="text-center">
                                        Here you can find all the tools and/or software I use, or I am familiar with.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-6">
                            <ul>
                                <li className="list-group-item"><a href="https://code.visualstudio.com">VSCode</a> &amp; <a
                                    href="https://marketplace.visualstudio.com/items?itemName=enkia.tokyo-night">Tokyo Night Theme</a></li>
                                <li className="list-group-item"><a href="https://www.jetbrains.com/idea">IntelliJ IDEA</a> &amp; <a
                                    href="https://plugins.jetbrains.com/plugin/12255-visual-studio-code-dark-plus-theme">Visual Studio Dark
                                    Plus
                                    Theme</a></li>
                                <li className="list-group-item"><a href="https://visualstudio.microsoft.com/vs">Visual Studio</a> &amp; <a
                                    href="https://www.jetbrains.com/resharper/">Resharper</a></li>
                                <li className="list-group-item"><a href="https://www.jetbrains.com/rider/">Rider</a></li>
                                <li className="list-group-item"><a href="https://notepad-plus-plus.org">Notepad++</a> &amp; <a
                                    href="https://github.com/hellon8/VS2019-Dark-Npp">VS2019 Dark NPP Theme</a></li>
                                <li className="list-group-item"><a href="https://datalore.jetbrains.com/">Datalore</a></li>
                                <li className="list-group-item"><a href="https://www.jetbrains.com/phpstorm/">PhpStorm</a></li>
                                <li className="list-group-item"><a href="https://www.autohotkey.com">AutoHotkey</a></li>
                                <li className="list-group-item"><a href="https://winscp.net">WinSCP</a></li>
                                <li className="list-group-item"><a href="https://stefansundin.github.io/altdrag">AltDrag</a></li>
                            </ul>
                        </div>
                        <div className="col-md-6">
                            <ul>
                                <li className="list-group-item"><a href="https://ueli.app">Ueli</a></li>
                                <li className="list-group-item"><a href="https://ditto-cp.sourceforge.io">Ditto</a></li>
                                <li className="list-group-item"><a href="https://unity.com/">Unity</a></li>
                                <li className="list-group-item"><a href="https://www.docker.com/">Docker</a></li>
                                <li className="list-group-item"><a href="https://www.postman.com/">Postman</a></li>
                                <li className="list-group-item"><a href="https://dbeaver.io/">DBeaver</a></li>
                                <li className="list-group-item"><a href="https://tableplus.com/">TablePlus</a></li>
                                <li className="list-group-item"><a href="https://www.jetbrains.com/datagrip/">DataGrip</a></li>
                                <li className="list-group-item"><a href="https://desktop.github.com/">Github Desktop</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>

    return html;
}

export default Tools;