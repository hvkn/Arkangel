var LocalStrategy = require('passport-local').Strategy;
var modelUser = require('../model/modelLogin');
var modelAvatar = require('../model/modelAvatar');

module.exports = function (passport) {
    passport.serializeUser(function (user, done) {
        done(null, user.id);
    });

    passport.deserializeUser(function (id, done) {
        modelUser.findById(id, function (err, user) {
            done(err, user);
        });
    });

    passport.use('local-signup', new LocalStrategy({
        usernameField: 'email',
        passwordField: 'password',
        passReqToCallback: true
    },
        function (req, email, password, done) {
            process.nextTick(function () {
                var reEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (reEmail.test(email) === false)
                    return done(null, false, req.flash('signupMessage', 'Wrong format email'));
                if (password.length <= 5)
                    return done(null, false, req.flash('signupMessage', 'Length is greater than 5'));
                modelUser.findOne({ 'local.email': email }, function (err, user) {
                    if (err)
                        return done(err);
                    if (user) {
                        return done(null, false, req.flash('signupMessage', 'Duplicate email!'));
                    } else {
                        var newUser = new modelUser();
                        newUser.local.email = email;
                        newUser.local.password = newUser.generateHash(password);
                        newUser.save(function (err) {
                            if (err)
                                throw err;
                            var data = {
                                user: newUser.local.email,
                                path: 'images/avatar/avatar.jpg'
                            }
                            var newAvatar = new modelAvatar(data);
                            newAvatar.save(err => {
                                if (err) console.log(err);
                            })
                            return done(null, newUser, req.flash('signupMessage', 'Welcome to Akangel'), req.flash('user', newUser.local.email));
                        });
                    }
                });
            });
        }));

    passport.use('local-login', new LocalStrategy({
        usernameField: 'email',
        passwordField: 'password',
        passReqToCallback: true
    },
        function (req, email, password, done) {
            process.nextTick(function () {
                modelUser.findOne({ 'local.email': email }, function (err, user) {
                    if (err)
                        return done(err);
                    if (!user)
                        return done(null, false, req.flash('loginMessage', 'Email not found!'));
                    if (!user.validPassword(password))
                        return done(null, false, req.flash('loginMessage', 'Wrong password!'));
                    return done(null, user, req.flash('loginMessage', 'Welcome to Akangel'), req.flash('user', user.local.email));
                });
            })

        })
    );
};