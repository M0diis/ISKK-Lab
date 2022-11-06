/**
 * Prints some information about the app. React component.
 * @returns Component HTML.
 */
function About() {
	// Render component HTML
	return (
		<>
		<br/>
		<div style={{ height: "80%" }} className="d-flex container justify-content-center align-items-center">
			<br/>
			<div className="row g-3 align-items-center">
				<div className="col-md-6 align-items-center">
						<div>
							<div className="card-body">
								<h5 className="card-title">Hi, my name is Modestas.</h5>
								<p className="card-text">
									I'm a computer science student who likes to code and build things.
								</p>
								<p className="card-text">I have experience with many programming languages, systems
									and always like to learn something new.
								</p>
								<p className="card-text">
									I am currently working as a programmer, developing software for Lithuanian energy.
								</p>
								<p className="card-text">
									I also work as a programmer in development of various software.
								</p>
								<p className="card-text"><small className="text-muted"></small></p>
							</div>
						</div>
					</div>
					<div className="col-md-6 align-items-center">
						<div className="text-center">
							<img src="https://github.com/M0diis/M0diis/raw/output/generated/overview.svg" alt="Github statistika" height="290px" width="470px"></img>
						</div>
					</div>
				</div>
		</div>
		</>
	);
}

export default About;