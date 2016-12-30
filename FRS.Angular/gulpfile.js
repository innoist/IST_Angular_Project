/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
  rimraf = require("rimraf"),
  concat = require("gulp-concat"),
  cssmin = require("gulp-cssmin"),
  uglify = require("gulp-uglify");

var paths = {
    webroot: "./app/"
};

paths.systemJs = paths.webroot + "js/*.js";
paths.basejs = paths.webroot + "js/base.js";
paths.excludeMinJs = "!" + paths.webroot + "js/*.min.js";
paths.baseMinjs = paths.webroot + "js/base.min.js";
paths.minJs = paths.webroot + "js/*.min.js";
paths.css = paths.webroot + "css/*.css";
paths.minCss = paths.webroot + "css/*.min.css";
paths.concatJsDest = paths.webroot + "js/system.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:base.min.js", function (cb) {
    rimraf(paths.baseMinjs, cb);
});

gulp.task("clean:system.min.js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean", ["clean:base.min.js", "clean:system.min.js"]);

gulp.task("min:systemjs", function () {
    return gulp.src(["./app/js/Modules.js", "./app/js/app.js", "./app/js/lazymodules.js", "./app/js/Config.js", paths.excludeMinJs, "!" + paths.basejs])
      .pipe(concat(paths.concatJsDest))
      .pipe(uglify())
      .pipe(gulp.dest("."));
});

gulp.task("min:basejs", function () {
    return gulp.src([paths.basejs, paths.excludeMinJs])
      .pipe(concat(paths.baseMinjs))
      .pipe(uglify())
      .pipe(gulp.dest("."));
});

//gulp.task("min:css", function () {
//    return gulp.src([paths.css, "!" + paths.minCss])
//      .pipe(concat(paths.concatCssDest))
//      .pipe(cssmin())
//      .pipe(gulp.dest("."));
//});

//gulp.task("min", ["min:js", "min:css"]);
//gulp.task("min", ["min:js"]);
