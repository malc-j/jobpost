<h1> JobPost Web API</h3>

<h3>About this</h3>
<p>This is one half of a bigger "hobby"-project created mainly to showcase my ability as a fullstack developer. </p>
<p> The goal is to create a job finding website of some sort, although I have not decided how far I want to go or how advanced it will be. The plan for now is to create two main projects, an web api connected to a relational database that communicates with a frontend web application that showcases and manipulates data from the database. However, this repo will mainly house and contain information about the backend part.</p>

<h3>Information and tech stack</h3>
<p>This is an <strong>ASP.NET Core REST Api</strong> connected to a <strong>MySQLServer</strong> database. The database and the tables were created through a code first approach, by first creating entities and defining their properties as classes and creating a migration. I created a SQL-Server through the <strong>Azure Portal</strong> and setup the connection strings so I could run the migration an create the database. When the database was created I created an <strong>Web/App Service</strong> project in the <strong>Azure Portal</strong> to deploy the project and host the REST api on Azure as well. While deploying I configured a simple <strong>CD-pipeline</strong> through a <strong>.yaml</strong> file with <strong>Github Actions</strong>. In the same solutIion but as an individual solution I have an <strong>NUnit<strong>-project for simple unit testing, together with som nuget packages for coverage and gathering test information.</p>

<h3>Future Plans</h3>
<p>The plan is to keep making this project more advanced and play with different techniques to further optimize and secure. Below I have a short list of future features:</p>
<ul>
  <li>Including the unit tests in the pipeline and and implement some kind of coverage rules before being able to merge to main</li>
  <li>Role based <strong>Authentication</strong></li>
  <li>Chat funcionality and <strong>Web Sockets</strong></li>
  <li>Proper documentation for database tables</li>
</ul>

I am happy to sen the links to the live api if you would like to try it out and make calls to the api. Just send me an email. Best Regards!

