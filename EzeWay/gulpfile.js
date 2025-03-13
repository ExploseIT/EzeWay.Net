/**
 
 Install node and npm from https://nodejs.org

 Copy boostrap, jquery etc to the wwwroot/dist folder from node_modules by running 'npm install' then 'npx gulp'
 
 Remove initially installed Bootstrap, jQuery by removing the css, js, and lib folders from the wwwroot folder

 Remove also _ValidationScriptsPartial.cshtml in Views/Shared

 Update the Bootstrap and jQuery links to put to those generated in the wwwroot/dist folder

 **/

const { src, dest, series } = require('gulp');
var clean = require('gulp-clean');
const fs = require('fs'); 

function bootstrapiconscopy(cb) {
    src('./node_modules/bootstrap-icons/**', { encoding: false })
        .pipe(dest('./wwwroot/dist/bootstrap-icons/'));
    cb();
}

 function jqueryuicopy(cb) {
     src('./node_modules/jquery-ui/dist/jquery-ui.min.js')
         .pipe(dest('./wwwroot/dist/jquery-ui/js/'));
     src('./node_modules/jquery-ui/dist/themes/smoothness/**')
         .pipe(dest('./wwwroot/dist/jquery-ui/css/smoothness/'));
         cb();
 }

 function bootstrapcopy(cb) {
     src('./node_modules/bootstrap/dist/css/bootstrap.min.*')
     .pipe(dest('./wwwroot/dist/bootstrap/css/'));
     src('./node_modules/bootstrap/dist/js/bootstrap.bundle.min.*')
     .pipe(dest('./wwwroot/dist/bootstrap/js/'));
     cb();
 }
 
 function jquerycopy(cb) {
     src('./node_modules/jquery/dist/**')
   .pipe(dest('./wwwroot/dist/jquery/'));
   cb();
 }


function fortawesomecopy(cb) {
    src('./node_modules/@fortawesome/fontawesome-free/css/all.css', { encoding: false })
        .pipe(dest('./wwwroot/dist/fontawesome-free/css/'));
    src('./node_modules/@fortawesome/fontawesome-free/webfonts/fa-regular-400.*', { encoding: false })
        .pipe(dest('./wwwroot/dist/fontawesome-free/webfonts/'));
    src('./node_modules/@fortawesome/fontawesome-free/webfonts/fa-solid-900.*', { encoding: false })
        .pipe(dest('./wwwroot/dist/fontawesome-free/webfonts/'));
    src('./node_modules/@fortawesome/fontawesome-free/webfonts/fa-brands-400.*', { encoding: false })
        .pipe(dest('./wwwroot/dist/fontawesome-free/webfonts/'));
    cb();
}
 
 function jqueryvalidationcopy(cb) {
     src('./node_modules/jquery-validation/dist/*.js')
         .pipe(dest('./wwwroot/dist/jquery-validation/'));
     cb();
 }
 
 
 // The `clean` function is not exported so it can be considered a private task.
 // It can still be used within the `series()` composition.
 function doclean(cb) {
     const distPath = './wwwroot/dist/';

     if (fs.existsSync(distPath)) {
         return src(distPath, { read: false, allowEmpty: true })
             .pipe(clean());
     } else {
         console.log(`Skipping clean: ${distPath} does not exist.`);
         cb(); // Call the callback to prevent Gulp from hanging
     }
   }
   
   // The `build` function is exported so it is public and can be run with the `gulp` command.
   // It can also be used within the `series()` composition.
   function build(cb) {
     // body omitted
     cb();
   }
   
exports.build = build;

exports.default = series(
    doclean
    , bootstrapiconscopy
     , fortawesomecopy
     , bootstrapcopy
    , jquerycopy
    , jqueryuicopy
    , jqueryvalidationcopy
 );

 exports.doclean = series(
    doclean
 );

