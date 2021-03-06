﻿using System.Text;
using NUnit.Framework;

namespace tplink_smartplug.tests
{
    [TestFixture]
    class TpLinkEncrypterTests
    {
        [TestCase("{\"system\":{\"set_relay_state\":{\"state\":1}}}", new byte[46]
        {
            0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x81, 0xf8, 0x8b, 0xff, 0x9a, 0xf7,
            0xd5, 0xef, 0x94, 0xb6, 0xc5, 0xa0, 0xd4, 0x8b, 0xf9, 0x9c, 0xf0, 0x91,
            0xe8, 0xb7, 0xc4, 0xb0, 0xd1, 0xa5, 0xc0, 0xe2, 0xd8, 0xa3, 0x81, 0xf2,
            0x86, 0xe7, 0x93, 0xf6, 0xd4, 0xee, 0xdf, 0xa2, 0xdf, 0xa2
        })]
        [TestCase("{\"system\":{\"set_relay_state\":{\"state\":0}}}", new byte[46]
        {
            0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x81, 0xf8, 0x8b, 0xff, 0x9a, 0xf7,
            0xd5, 0xef, 0x94, 0xb6, 0xc5, 0xa0, 0xd4, 0x8b, 0xf9, 0x9c, 0xf0, 0x91,
            0xe8, 0xb7, 0xc4, 0xb0, 0xd1, 0xa5, 0xc0, 0xe2, 0xd8, 0xa3, 0x81, 0xf2,
            0x86, 0xe7, 0x93, 0xf6, 0xd4, 0xee, 0xde, 0xa3, 0xde, 0xa3
        })]
        [TestCase("{\"system\":{\"reboot\":{\"delay\":1}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x81, 0xf8, 0x8b, 0xff, 0x9a, 0xf7, 0xd5, 0xef, 0x94, 0xb6, 0xc4, 0xa1, 0xc3, 0xac, 0xc3, 0xb7, 0x95, 0xaf, 0xd4, 0xf6, 0x92, 0xf7, 0x9b, 0xfa, 0x83, 0xa1, 0x9b, 0xaa, 0xd7, 0xaa, 0xd7 })]
        [TestCase("{\"system\":{\"get_sysinfo\":{}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x81, 0xf8, 0x8b, 0xff, 0x9a, 0xf7, 0xd5, 0xef, 0x94, 0xb6, 0xd1, 0xb4, 0xc0, 0x9f, 0xec, 0x95, 0xe6, 0x8f, 0xe1, 0x87, 0xe8, 0xca, 0xf0, 0x8b, 0xf6, 0x8b, 0xf6 })]
        [TestCase("{\"cnCloud\":{\"get_info\":{}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x91, 0xff, 0xbc, 0xd0, 0xbf, 0xca, 0xae, 0x8c, 0xb6, 0xcd, 0xef, 0x88, 0xed, 0x99, 0xc6, 0xaf, 0xc1, 0xa7, 0xc8, 0xea, 0xd0, 0xab, 0xd6, 0xab, 0xd6 })]
        [TestCase("{\"netif\":{\"get_scaninfo\":{\"refresh\":0}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x9c, 0xf9, 0x8d, 0xe4, 0x82, 0xa0, 0x9a, 0xe1, 0xc3, 0xa4, 0xc1, 0xb5, 0xea, 0x99, 0xfa, 0x9b, 0xf5, 0x9c, 0xf2, 0x94, 0xfb, 0xd9, 0xe3, 0x98, 0xba, 0xc8, 0xad, 0xcb, 0xb9, 0xdc, 0xaf, 0xc7, 0xe5, 0xdf, 0xef, 0x92, 0xef, 0x92 })]
        [TestCase("{\"time\":{\"get_time\":{}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x86, 0xef, 0x82, 0xe7, 0xc5, 0xff, 0x84, 0xa6, 0xc1, 0xa4, 0xd0, 0x8f, 0xfb, 0x92, 0xff, 0x9a, 0xb8, 0x82, 0xf9, 0x84, 0xf9, 0x84 })]
        [TestCase("{\"schedule\":{\"get_rules\":{}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x81, 0xe2, 0x8a, 0xef, 0x8b, 0xfe, 0x92, 0xf7, 0xd5, 0xef, 0x94, 0xb6, 0xd1, 0xb4, 0xc0, 0x9f, 0xed, 0x98, 0xf4, 0x91, 0xe2, 0xc0, 0xfa, 0x81, 0xfc, 0x81, 0xfc })]
        [TestCase("{\"count_down\":{\"get_rules\":{}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x91, 0xfe, 0x8b, 0xe5, 0x91, 0xce, 0xaa, 0xc5, 0xb2, 0xdc, 0xfe, 0xc4, 0xbf, 0x9d, 0xfa, 0x9f, 0xeb, 0xb4, 0xc6, 0xb3, 0xdf, 0xba, 0xc9, 0xeb, 0xd1, 0xaa, 0xd7, 0xaa, 0xd7 })]
        [TestCase("{\"anti_theft\":{\"get_rules\":{}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x93, 0xfd, 0x89, 0xe0, 0xbf, 0xcb, 0xa3, 0xc6, 0xa0, 0xd4, 0xf6, 0xcc, 0xb7, 0x95, 0xf2, 0x97, 0xe3, 0xbc, 0xce, 0xbb, 0xd7, 0xb2, 0xc1, 0xe3, 0xd9, 0xa2, 0xdf, 0xa2, 0xdf })]
        [TestCase("{\"system\":{\"reset\":{\"delay\":1}}}", new byte[] { 0x00, 0x00, 0x00, 0x00, 0xd0, 0xf2, 0x81, 0xf8, 0x8b, 0xff, 0x9a, 0xf7, 0xd5, 0xef, 0x94, 0xb6, 0xc4, 0xa1, 0xd2, 0xb7, 0xc3, 0xe1, 0xdb, 0xa0, 0x82, 0xe6, 0x83, 0xef, 0x8e, 0xf7, 0xd5, 0xef, 0xde, 0xa3, 0xde, 0xa3 })]
        public void EncryptionTests(string original, byte[] expectedResult)
        {
            var sut = new TpLinkAutokeyCipher();

            var originalBytes = Encoding.Default.GetBytes(original);

            var encryptedCmd = sut.Encrypt(originalBytes);

            CollectionAssert.AreEqual(expectedResult, encryptedCmd);
        }

        [TestCase("{\"system\":{\"set_relay_state\":{\"state\":1}}}")]
        [TestCase("{\"system\":{\"set_relay_state\":{\"state\":0}}}")]
        [TestCase("{\"system\":{\"reboot\":{\"delay\":1}}}")]
        [TestCase("{\"system\":{\"get_sysinfo\":{}}}")]
        [TestCase("{\"cnCloud\":{\"get_info\":{}}}")]
        [TestCase("{\"netif\":{\"get_scaninfo\":{\"refresh\":0}}}")]
        [TestCase("{\"time\":{\"get_time\":{}}}")]
        [TestCase("{\"schedule\":{\"get_rules\":{}}}")]
        [TestCase("{\"count_down\":{\"get_rules\":{}}}")]
        [TestCase("{\"anti_theft\":{\"get_rules\":{}}}")]
        [TestCase("{\"system\":{\"reset\":{\"delay\":1}}}")]

        public void EncryptionDecryptionTests(string original)
        {
            var sut = new TpLinkAutokeyCipher();

            var bytes = Encoding.Default.GetBytes(original);

            var encrypted = sut.Encrypt(bytes);
            var decrypted = sut.Decrypt(encrypted);

            var decryptedString = Encoding.Default.GetString(decrypted);

            Assert.AreEqual(original, decryptedString);
        }
    }
}