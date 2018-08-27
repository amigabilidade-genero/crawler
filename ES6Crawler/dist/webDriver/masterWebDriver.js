'use strict';

Object.defineProperty(exports, "__esModule", {
  value: true
});

require('chromedriver');

var _chrome = require('selenium-webdriver/chrome');

var _chrome2 = _interopRequireDefault(_chrome);

var _seleniumWebdriver = require('selenium-webdriver');

var _assert = require('assert');

var _assert2 = _interopRequireDefault(_assert);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _asyncToGenerator(fn) { return function () { var gen = fn.apply(this, arguments); return new Promise(function (resolve, reject) { function step(key, arg) { try { var info = gen[key](arg); var value = info.value; } catch (error) { reject(error); return; } if (info.done) { resolve(value); } else { return Promise.resolve(value).then(function (value) { step("next", value); }, function (err) { step("throw", err); }); } } return step("next"); }); }; }

var URL = 'https://www.jusbrasil.com.br';

var chromeOptions = new _chrome2.default.Options();
chromeOptions.addArguments('test-type');
// chromeOptions.addArguments('start-maximized')
chromeOptions.addArguments('--js-flags=--expose-gc');
chromeOptions.addArguments('--enable-precise-memory-info');
chromeOptions.addArguments('--disable-popup-blocking');
chromeOptions.addArguments('--disable-default-apps');
chromeOptions.addArguments('--disable-infobars');
chromeOptions.addArguments('--incognito');
chromeOptions.addArguments('--disable-extensionss');
chromeOptions.addArguments('--no-sandbox');
chromeOptions.addArguments('test-type=browser');

var initBrowser = function initBrowser() {
  var driver;
  try {
    driver = new _seleniumWebdriver.Builder().forBrowser('chrome').withCapabilities(chromeOptions).build();
    driver.manage().deleteAllCookies();
    return driver;
  } catch (error) {
    console.log('Server error: Ocorreu um erro inesperado na inicialização do arquivo binário respectivo ao navegador.');
    console.log('Erro interno: ', error);
    driver.close();
  }
};

var driver = initBrowser();

var MasterWebDriver = function MasterWebDriver() {
  console.log('Initialized MasterWebDriver');
  Steps();
  // versionSite(VERSION)
};

var Steps = function () {
  var _ref = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee() {
    return regeneratorRuntime.wrap(function _callee$(_context) {
      while (1) {
        switch (_context.prev = _context.next) {
          case 0:
            _context.next = 2;
            return fetchUrl();

          case 2:
            _context.next = 4;
            return sign();

          case 4:
          case 'end':
            return _context.stop();
        }
      }
    }, _callee, undefined);
  }));

  return function Steps() {
    return _ref.apply(this, arguments);
  };
}();

var fetchUrl = function () {
  var _ref2 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee2() {
    return regeneratorRuntime.wrap(function _callee2$(_context2) {
      while (1) {
        switch (_context2.prev = _context2.next) {
          case 0:
            _context2.next = 2;
            return driver.get(URL);

          case 2:
          case 'end':
            return _context2.stop();
        }
      }
    }, _callee2, undefined);
  }));

  return function fetchUrl() {
    return _ref2.apply(this, arguments);
  };
}();

var sign = function () {
  var _ref3 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee3() {
    return regeneratorRuntime.wrap(function _callee3$(_context3) {
      while (1) {
        switch (_context3.prev = _context3.next) {
          case 0:
            _context3.next = 2;
            return driver.findElement(_seleniumWebdriver.By.className('btn btn--flat btn-login')).click();

          case 2:
            _context3.next = 4;
            return driver.findElement(_seleniumWebdriver.By.id('form-sign-up-email')).sendKeys('amigabilidade.genero.ifmg@gmail.com');

          case 4:
            _context3.next = 6;
            return driver.findElement(_seleniumWebdriver.By.id('form-sign-up-password')).sendKeys('@amigabilidade#genero');

          case 6:
            _context3.next = 8;
            return driver.findElement(_seleniumWebdriver.By.name('sign-up-button')).submit();

          case 8:
          case 'end':
            return _context3.stop();
        }
      }
    }, _callee3, undefined);
  }));

  return function sign() {
    return _ref3.apply(this, arguments);
  };
}();

var customSearchConfig = function () {
  var _ref4 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee4() {
    var customSearch;
    return regeneratorRuntime.wrap(function _callee4$(_context4) {
      while (1) {
        switch (_context4.prev = _context4.next) {
          case 0:
            _context4.next = 2;
            return driver.findElements(_seleniumWebdriver.By.className('navbar-link'));

          case 2:
            customSearch = _context4.sent;
            _context4.next = 5;
            return customSearch[3].click();

          case 5:
            _context4.next = 7;
            return driver.findElement(_seleniumWebdriver.By.id('radio-clear')).click();

          case 7:
            _context4.next = 9;
            return driver.findElements(_seleniumWebdriver.By.className('dropdown-toggle'));

          case 9:
            customSearch = _context4.sent;
            _context4.next = 12;
            return customSearch[2].click();

          case 12:
            _context4.next = 14;
            return driver.findElement(_seleniumWebdriver.By.id('10000575')).click();

          case 14:
            _context4.next = 16;
            return customSearch[2].click();

          case 16:
            search();

          case 17:
          case 'end':
            return _context4.stop();
        }
      }
    }, _callee4, undefined);
  }));

  return function customSearchConfig() {
    return _ref4.apply(this, arguments);
  };
}();

var search = function () {
  var _ref5 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee5() {
    var keys, btn;
    return regeneratorRuntime.wrap(function _callee5$(_context5) {
      while (1) {
        switch (_context5.prev = _context5.next) {
          case 0:
            keys = 'assédio sexual';
            _context5.next = 3;
            return driver.findElement(_seleniumWebdriver.By.name('q'));

          case 3:
            btn = _context5.sent;

            btn.sendKeys(keys, _seleniumWebdriver.Key.ENTER);

          case 5:
          case 'end':
            return _context5.stop();
        }
      }
    }, _callee5, undefined);
  }));

  return function search() {
    return _ref5.apply(this, arguments);
  };
}();

var navigateInDocuments = function navigateInDocuments() {
  var webElements = driver.findElements(_seleniumWebdriver.By.className('title small'));
  webElements.then(function () {
    var _ref6 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee7(element) {
      return regeneratorRuntime.wrap(function _callee7$(_context7) {
        while (1) {
          switch (_context7.prev = _context7.next) {
            case 0:
              if (!(element.length > 0)) {
                _context7.next = 11;
                break;
              }

              // rotina 1
              console.log('rotina 1');
              element[0].findElement(_seleniumWebdriver.By.tagName('a')).click();
              _context7.next = 5;
              return driver.sleep(3000);

            case 5:
              _context7.next = 7;
              return driver.findElement(_seleniumWebdriver.By.className('JurisprudenceDecisionTabs-item btn btn--light')).click();

            case 7:
              _context7.next = 9;
              return extractInfo();

            case 9:
              _context7.next = 14;
              break;

            case 11:
              // rotina 2
              console.log('rotina 2');
              webElements = driver.findElements(_seleniumWebdriver.By.className('BaseSnippetWrapper-title'));
              webElements.then(function () {
                var _ref7 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee6(element) {
                  return regeneratorRuntime.wrap(function _callee6$(_context6) {
                    while (1) {
                      switch (_context6.prev = _context6.next) {
                        case 0:
                          element[0].findElement(_seleniumWebdriver.By.tagName('a')).click();
                          _context6.next = 3;
                          return driver.sleep(3000);

                        case 3:
                          _context6.next = 5;
                          return driver.findElement(_seleniumWebdriver.By.className('JurisprudenceDecisionTabs-item btn btn--light')).click();

                        case 5:
                          driver.sleep(3000);
                          _context6.next = 8;
                          return extractInfo();

                        case 8:
                        case 'end':
                          return _context6.stop();
                      }
                    }
                  }, _callee6, undefined);
                }));

                return function (_x2) {
                  return _ref7.apply(this, arguments);
                };
              }());

            case 14:
            case 'end':
              return _context7.stop();
          }
        }
      }, _callee7, undefined);
    }));

    return function (_x) {
      return _ref6.apply(this, arguments);
    };
  }());
};

var extractInfo = function () {
  var _ref8 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee8() {
    return regeneratorRuntime.wrap(function _callee8$(_context8) {
      while (1) {
        switch (_context8.prev = _context8.next) {
          case 0:
            _context8.next = 2;
            return driver.findElement(_seleniumWebdriver.By.className('JurisprudencePage-content anon-content')).then(function (text) {
              console.log(text.getText());
            });

          case 2:
          case 'end':
            return _context8.stop();
        }
      }
    }, _callee8, undefined);
  }));

  return function extractInfo() {
    return _ref8.apply(this, arguments);
  };
}();

var writeJson = function writeJson(textContent) {
  var doc = {
    content: textContent
  };
  var jsonText = JSON.stringify(doc);
  document.write(jsonText);
};

exports.default = MasterWebDriver;
//# sourceMappingURL=masterWebDriver.js.map