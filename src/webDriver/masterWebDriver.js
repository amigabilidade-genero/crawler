import 'chromedriver'
import chrome from 'selenium-webdriver/chrome'
import { Builder, By, Key, until } from 'selenium-webdriver'
import assert from 'assert'

const URL = 'https://www.jusbrasil.com.br'

const chromeOptions = new chrome.Options()
chromeOptions.addArguments('test-type')
chromeOptions.addArguments('start-maximized')
chromeOptions.addArguments('--js-flags=--expose-gc')
chromeOptions.addArguments('--enable-precise-memory-info')
chromeOptions.addArguments('--disable-popup-blocking')
chromeOptions.addArguments('--disable-default-apps')
chromeOptions.addArguments('--disable-infobars')
chromeOptions.addArguments('--incognito')
chromeOptions.addArguments('--disable-extensionss')
chromeOptions.addArguments('--no-sandbox')
chromeOptions.addArguments('test-type=browser')

const initBrowser = () => {
  var driver
  try {
    driver = new Builder()
      .forBrowser('chrome')
      .withCapabilities(chromeOptions)
      .build()
    driver.manage().deleteAllCookies()
    return driver
  } catch (error) {
    console.log('Server error: Ocorreu um erro inesperado na inicialização do arquivo binário respectivo ao navegador.'
    )
    console.log('Erro interno: ', error)
    driver.close()
  }
}

const driver = initBrowser()

const MasterWebDriver = () => {
  console.log('Initialized MasterWebDriver')
  Steps()
  // versionSite(VERSION)
}

const Steps = async () => {
  await fetchUrl()
  // await sign()
  await driver.sleep(3000)
  await customSearchConfig()
  await driver.sleep(3000)

  var webElements = driver.findElements(By.className('title small'))
  webElements.then(async (element) => {
    if (element.length > 0) {
      // rotina 1
      console.log('rotina 1')
      element[0].findElement(By.tagName('a')).click()
      var inteiroTeor = await webElements.findElements(By.className('JurisprudenceDecisionTabs'))
      console.log(inteiroTeor.length)
    } else {
      // rotina 2
      console.log('rotina 2')
      webElements = driver.findElements(By.className('BaseSnippetWrapper-title'))
      webElements.then(async (element) => {
        element[0].findElement(By.tagName('a')).click()
        var inteiroTeor = await webElements.findElements(By.className('JurisprudenceDecisionTabs'))
        console.log(inteiroTeor.length)
      })
    }
  })
}

function printConsole (string) {
  return string
}

const fetchUrl = async () => {
  await driver.get(URL)
}

const sign = async () => {
  // SignIn no JusBrasil
  await driver.findElement(By.className('btn btn--flat btn-login')).click()
  await driver.findElement(By.id('form-sign-up-email')).sendKeys('amigabilidade.genero.ifmg@gmail.com')
  await driver.findElement(By.id('form-sign-up-password')).sendKeys('@amigabilidade#genero')
  await driver.findElement(By.name('sign-up-button')).submit()
}

const customSearchConfig = async () => {
  // Seleciona Jurisprudência
  var customSearch = await driver.findElements(By.className('navbar-link'))
  await customSearch[3].click()
  // Limpa opções de órgãos judiciais
  await driver.findElement(By.id('radio-clear')).click()
  // Seleciona tribunal
  customSearch = await driver.findElements(By.className('dropdown-toggle'))
  await customSearch[2].click()
  await driver.findElement(By.id('10000575')).click()
  await customSearch[2].click()
  search()
}

const search = async () => {
  var keys = 'assédio sexual'
  var btn = await driver.findElement(By.name('q'))
  btn.sendKeys(keys, Key.ENTER)
}

// const getDocuments1 = async () => {
//   var searchForm = driver.findElement(By.className('title small'))
//   searchForm = searchForm.findElement(By.tagName('a'))
//   searchForm.click()
//   searchForm = searchForm.findElement(By.className('JurisprudenceDecisionTabs-item btn active'))
//   searchForm.click()
// }

// const getDocuments2 = async () => {
//   var searchForm = await driver.findElement(By.className('BaseSnippetWrapper-title'))
//   searchForm = searchForm.findElement(By.tagName('a'))
//   searchForm.click()
//   // var ij = await driver.findElements(By.className('i juris'))
//   // console.log(ij[1])
// }

export default MasterWebDriver
