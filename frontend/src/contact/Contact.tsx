/**
 * Prints some information about the app. React component.
 * @returns Component HTML.
 */
function Contact()
{
	// Render component HTML
	const html =
		<>
			<br/>
			<div className="container">
				<section className="mb-4">
					<h2 className="h1-responsive font-weight-bold text-center my-4">CONTACT ME</h2>

					<p className="text-center w-responsive mx-auto mb-3">Do you have any questions? I am a freelancer for your various projects. I work with both front-end and back-end parts.</p>
					<p className="text-center w-responsive mx-auto">Write me a letter or get in touch with the given contacts!</p>

					<div className="row">

						<div className="col-md-9 mb-md-0 mb-5">
							<form id="contact-form" name="contact-form" action="" method="POST">

								<div className="row">
									<div className="col-md-6">
										<div className="md-form mb-0">
											<label className="">Your name</label>
											<input type="text" id="name" name="name" className="form-control"></input>
										</div>
									</div>

									<div className="col-md-6">
										<div className="md-form mb-0">
											<label className="">Email</label>
											<input type="text" id="email" name="email" className="form-control"></input>
										</div>
									</div>
								</div>

								<br/>


								<div className="row">

									<div className="col-md-12">
										<div className="md-form mb-0">
											<label className="">Subject</label>
											<input type="text" id="subject" name="subject" className="form-control"></input>
										</div>
									</div>
								</div>

								<br/>

								<div className="row">
									<div className="col-md-12">
										<div className="md-form">
											<label>Message</label>
											<textarea inputMode="text" id="message" name="message" rows={2} className="form-control md-textarea"></textarea>
										</div>
									</div>
								</div>
								
								<br/>

								<div className="text-center text-md-left">
									<button type="submit" className="btn btn-primary">Send</button>
								</div>
							</form>
						</div>

						<br/>

						<div className="col-md-3">
							<br/>
							<ul className="list-unstyled mb-0">
								<ul className="contact-info">
									<li><i className="fa fa-phone"><a href="tel:+37063660905">+37063660905</a></i></li>
									<li><i className="fa fa-envelope"><a href="mailto:modestaskazlauskas1@gmail.com" target="_blank" rel="noreferrer">modestaskazlauskas1@gmail.com</a></i></li>
									<li><i className="fa fa-linkedin"><a href="https://www.linkedin.com/in/modestas-kaz/" target="_blank" rel="noreferrer">modestas-kaz</a></i></li>
									<li><i className="fa fa-github"><a href="https://github.com/M0diis" target="_blank" rel="noreferrer">M0diis</a></i></li>
								</ul>
							</ul>
						</div>
					</div>

				</section>
			</div>
		</>

	return html;
}

export default Contact;