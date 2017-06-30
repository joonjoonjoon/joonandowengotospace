extern "C" {
	
	char* cStringCopy(const char* string)
	{
		if (string == NULL)
			return NULL;
		char* res = (char*)malloc(strlen(string) + 1);
		strcpy(res, string);
		return res;
	}

	char* _GlitchGetLocale () {
		NSLocale *currentLocale = [NSLocale currentLocale];  // get the current locale.
		NSString *languageCode = [currentLocale objectForKey:NSLocaleIdentifier];
		NSLog(@"[GlitchNative] Langcode: %@",languageCode);
        return cStringCopy([languageCode UTF8String]);
	}
}