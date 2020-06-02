# ActiveIS.UmbracoForms.CustomEmail

A customizable email template which does not include the form data.

You can add multiple Reply-to, To, Bcc, Cc addresses, sends both HTML and plain text versions and uses an RTE for the email body.

The email template is fully customizable, and the template name can be input via the Umbraco backoffice.

## Getting started

This package is supported on Umbraco 8.6+ and Umbraco Forms 8.4.1+.

### Installation

ActiveIS.UmbracoForms.CustomEmail is available from [Our Umbraco](https://our.umbraco.com/packages/website-utilities/activeisumbracoformscustomemail/), [NuGet](https://www.nuget.org/packages/ActiveIS.UmbracoForms.CustomEmail) or as a manual download directly from GitHub.

**This package requires [Mw.UmbForms.Rte](https://github.com/Matthew-Wise/umbraco-forms-rte), this can be installed from [Our Umbraco](https://our.umbraco.com/packages/backoffice-extensions/umbraco-forms-rich-text/) or [NuGet](https://www.nuget.org/packages/Mw.UmbracoForms.Rte/)**

#### Our Umbraco repository
You can find a downloadable package on the [Our Umbraco](https://our.umbraco.com/packages/website-utilities/activeisumbracoformscustomemail/) site.

## Usage

This package adds a customizable email template to Umbraco Forms.

This is tested with **Umbraco V8.6.1** and **Umbraco Forms 8.4.1**

## Changelog

## [v1.1.2] - 2020-06-02
### Fixed
* Added "sender" header to stop "on behalf of" from name when using MailGun or SendGrid

## [v1.1.1] - 2020-06-02
### Changed
* Changed the template name setting description to reflect it's now a tree picker

### Fixed
* Removed cshtml from tree response as Razor views cannot be parsed


## [v1.1.0] - 2020-06-01
### Changed
**This contains breaking changes!**
* Reworked the template picker to use a tree instead of a text input

### Contribution guidelines

To raise a new bug, create an issue on the GitHub repository. To fix a bug or add new features, fork the repository and send a pull request with your changes. Feel free to add ideas to the repository's issues list if you would to discuss anything related to the package.

## License

Copyright &copy; 2020 [ActiveIS](https://activeis.net) & [Aaron Sadler](https://aaronsadler.uk).

Licensed under the MIT License.
