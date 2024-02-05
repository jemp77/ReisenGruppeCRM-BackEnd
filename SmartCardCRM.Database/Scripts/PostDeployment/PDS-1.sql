BEGIN TRY
	BEGIN TRAN
	DECLARE @HosterReadOnlyRoleId INT
	DECLARE @DirectorRoleId INT

	UPDATE Role
	SET Scopes = 'HasHostessAccess,CanCreateClient,CanCreateContract,CanEditClient,CanEditContract,CanEditClientLinner,CanEditClientCloser,CanDownloadContract,CanCancelContract,CanReadClient'
	WHERE RoleName = 'Hoster'

	UPDATE Role
	SET RoleName = 'Administrator'
	WHERE RoleName = 'Administrador'

	UPDATE Role
	SET RoleName = 'Bookings'
	WHERE RoleName = 'Reservas'

	UPDATE Role
	SET RoleName = 'BookingsAdmin'
	WHERE RoleName = 'AdminReservas'

	UPDATE Role
	SET RoleName = 'ClientService'
	WHERE RoleName = 'ServicioAlCliente'

	UPDATE Role
	SET RoleName = 'ClientServiceAdmin'
	WHERE RoleName = 'AdminServicioAlCliente'


	IF NOT EXISTS(SELECT 1 FROM Role WHERE RoleName = 'HosterReadOnly')
	BEGIN
		INSERT INTO Role
		VALUES('HosterReadOnly', 'HasHostessAccess,CanEditClientTlmkCode,CanReadClient,CanReadContract,HasAdministrationAccess,HasManifestAccess')
	END

	IF NOT EXISTS(SELECT 1 FROM Role WHERE RoleName = 'Director')
	BEGIN
		INSERT INTO Role
		VALUES('Director', 'CanReadClient,CanEditClientLinner,CanEditClientCloser')
	END


	SELECT @HosterReadOnlyRoleId = Id
	FROm Role
	WHERE RoleName = 'HosterReadOnly'

	SELECT @DirectorRoleId = Id
	FROm Role
	WHERE RoleName = 'Director'

	UPDATE UR
	SET UR.RoleId = @HosterReadOnlyRoleId
	FROM [User] U
	INNER JOIN UserRoles UR
	ON U.Id = UR.UserId
	INNER JOIN Role R
	ON UR.RoleId = R.Id
	WHERE UserName = 'Mercadeo'

	IF NOT EXISTS(SELECT 1 FROM [User] WHERE UserName = 'DirectorVentas')
	BEGIN
		INSERT INTO [User]
		VALUES('DirectorVentas', 'Smart354', 1, 'Jose', 'Luis')

		INSERT INTO UserRoles
		VALUES(@@IDENTITY, @DirectorRoleId)
	END
	
	UPDATE ConfigurationSettings
	SET [Value] = '2.0.2'
	WHERE [Key] = 'AppVersion'

	COMMIT TRAN
END TRY
BEGIN CATCH
	ROLLBACK TRAN
END CATCH