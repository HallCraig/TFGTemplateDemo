// <copyright file="GetUserResponse.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the GetUserResponse class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    #endregion

    /// <summary>
    /// Defines the ActiveDirectoryUser class.
    /// </summary>
    /// <remarks>
    /// Not applicable.
    /// </remarks>
    public class GetUserResponse
    {        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ActiveDirectoryUser class.
        /// </summary>
        public GetUserResponse()
        {
            this.AuthorizationGroups = new Collection<string>();
            this.DistributionGroups = new Collection<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the primary key. Example: Joe Soap
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the authorization groups. This includes authorization groups which the user is a member of indirectly.
        /// </summary>
        public ICollection<string> AuthorizationGroups { get; set; }

        /// <summary>
        /// Gets or sets the city. Example: Cape Town
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the company name. Example: TFG Infotec
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country code. Example: ZA
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country name. Example: South Africa
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the department name. Example: Development Architecture
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the displayed name. Example: Joe Soap
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the distribution groups. This does not include distribution groups which the user is a member of indirectly.
        /// </summary>
        public ICollection<string> DistributionGroups { get; set; }

        /// <summary>
        /// Gets or sets the email address. Example: JoeS@ho.fosltd.co.za
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the email alias. Example: JoeS
        /// </summary>
        /// <value>
        /// The email alias.
        /// </value>
        public string EmailAlias { get; set; }

        /// <summary>
        /// Gets or sets the employee number. Example: 999999
        /// </summary>
        /// <value>
        /// The employee id.
        /// </value>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name. Example: Joe
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the full name. Example: Joe Soap
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the home page. Example: http://mysite.ho.fosltd.co.za/personal/joes/Blog
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or sets the initials. Example: J
        /// </summary>
        /// <value>
        /// The initials.
        /// </value>
        public string Initials { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has been authenticated successfully or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is authenticated]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the user logon name. Example: JoeS
        /// </summary>
        /// <value>
        /// The name of the log on.
        /// </value>
        public string LogOnName { get; set; }

        /// <summary>
        /// Gets or sets the manager name. Example: Joe Soap's Manager
        /// </summary>
        /// <value>
        /// The name of the manager.
        /// </value>
        public string ManagerName { get; set; }

        /// <summary>
        /// Gets or sets the office location. Example: Lefic 1
        /// </summary>
        /// <value>
        /// The office location.
        /// </value>
        public string OfficeLocation { get; set; }

        /// <summary>
        /// Gets or sets the postal address. Example: P.O. Box 6020
        /// </summary>
        /// <value>
        /// The postal address.
        /// </value>
        public string PostalAddress { get; set; }

        /// <summary>
        /// Gets or sets the postal code. Example: 7500
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the street address. Example: 342 Voortrekker Road
        /// </summary>
        /// <value>
        /// The street address.
        /// </value>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the suburb. Example: Parow East
        /// </summary>
        /// <value>
        /// The suburb.
        /// </value>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the surname. Example: Soap
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the telephone number. Example: (021) 555 4567
        /// </summary>
        /// <value>
        /// The telephone number.
        /// </value>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the title. Example: Mr
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Adds the authorization groups.
        /// </summary>
        /// <param name="groups">The authorization groups to add.</param>
        public void AddAuthorizationGroupRange(ICollection<string> groups)
        {
            if (groups == null)
            {
                throw new ArgumentNullException("groups");
            }

            foreach (var group in groups)
            {
                this.AuthorizationGroups.Add(group);
            }
        }

        /// <summary>
        /// Adds the distribution groups.
        /// </summary>
        /// <param name="groups">The distribution groups to add.</param>
        public void AddDistributionGroupRange(ICollection<string> groups)
        {
            if (groups == null)
            {
                throw new ArgumentNullException("groups");
            }

            foreach (var group in groups)
            {
                this.DistributionGroups.Add(group);
            }
        }

        #endregion

        #endregion
    }
}
